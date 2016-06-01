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

import com.example.pirateboat.productiontablet.data.Ip;
import com.example.pirateboat.productiontablet.data.Order;
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

    public RestfulHandler() throws MalformedURLException {
        Log.i(TAG,"resthandler created");

        if(url == null){
            //url = new URL("http://ip.jsontest.com");
           url = new URL("http://10.176.164.150:8080/RestService.svc/getOrder/w0000520");
        }


        try {
            urlConnection = (HttpURLConnection) url.openConnection();
            urlConnection.setReadTimeout(10000);
            urlConnection.setConnectTimeout(20000);
            urlConnection.setRequestMethod("GET");
            urlConnection.setDoInput(true);
            urlConnection.setDoOutput(false);
            //urlConnection.setChunkedStreamingMode(0);

        } catch (IOException e) {
            e.printStackTrace();
        }
        readStream();
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


    private void readStream() {
        InputStream is = null;

            try {
                urlConnection.setRequestMethod("GET");
                urlConnection.connect();

                is = urlConnection.getInputStream();
                InputStreamReader reader = new InputStreamReader(is, charset);
                OrderResult o = gson.fromJson(reader, OrderResult.class);
                Log.i(TAG,"hej");


            } catch (Exception ioe) {
                ioe.printStackTrace();
            } finally {
                urlConnection.disconnect();


            }

    }


    private void writeStream(String output) {

        try {
            urlConnection.connect();
            urlConnection = (HttpURLConnection) url.openConnection();
            out = urlConnection.getOutputStream();
            String questionnairesJson = gson.toJson(output);
            out.write(questionnairesJson.getBytes(Charset.forName("UTF-8")));

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