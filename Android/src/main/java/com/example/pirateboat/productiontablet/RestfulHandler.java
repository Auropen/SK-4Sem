package com.example.pirateboat.productiontablet;

import android.os.AsyncTask;
import android.util.Log;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.nio.charset.Charset;

import com.example.pirateboat.productiontablet.data.OrderResult;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

/**
 * Created by Swodah on 25-05-2016.
 */
public class RestfulHandler extends AsyncTask<Void, Void, Void> {
    private static final String TAG = "Production tablet";
    private URL url;
    OutputStream out;
    HttpURLConnection urlConnection = null;
    private static Gson gson = new GsonBuilder().create();
    public final Charset charset = Charset.forName("UTF-8");
    int attemptcounter = 0;
    public RestfulHandler() throws MalformedURLException {
        Log.i(TAG,"resthandler created");

        if(url == null){
           //url = new URL("http://keddebock.dk:8080/RestService.svc/getOrder/w0000520");
           //url = new URL("http://10.176.160.151:8080/RestService.svc/getOrder/w0000520");
           url = new URL ("http://10.176.160.151:8080/RestService.svc/getAllActiveOrders");
        }

    }

    @Override
    protected Void doInBackground(Void... params) {
        return null;
    }


    public URL getUrl() {
        return url;
    }

    public void setUrl(URL url) {
        this.url = url;
    }


    public OrderResult readStream() {
        InputStream is = null;
        OrderResult o = null;
            try {
                urlConnection = (HttpURLConnection) url.openConnection();
                urlConnection.setReadTimeout(10000);
                urlConnection.setConnectTimeout(20000);
                urlConnection.setRequestMethod("GET");
                urlConnection.setDoInput(true);
                urlConnection.setDoOutput(false);
                urlConnection.connect();
                is = urlConnection.getInputStream();
                InputStreamReader reader = new InputStreamReader(is, charset);
                o = gson.fromJson(reader, OrderResult.class);



            } catch (Exception ioe) {
                ioe.printStackTrace();
            } finally {
                urlConnection.disconnect();


            }
        if (o == null){
            if(attemptcounter<3){
                attemptcounter++;
                readStream();
            }
        }
        return o;
    }


    public void writeStream(OrderResult output) {

        try {
            urlConnection = (HttpURLConnection) url.openConnection();
            urlConnection.setReadTimeout(10000);
            urlConnection.setConnectTimeout(20000);
            urlConnection.setRequestMethod("POST");
            urlConnection.setDoInput(false);
            urlConnection.setDoOutput(true);
            urlConnection.connect();
            urlConnection = (HttpURLConnection) url.openConnection();
            out = urlConnection.getOutputStream();
            String orJson = gson.toJson(output);
            out.write(orJson.getBytes(Charset.forName("UTF-8")));

            int response = urlConnection.getResponseCode();
            Log.d(TAG, "Response: " + response);
            out.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }finally {
            urlConnection.disconnect();
        }
    }
}