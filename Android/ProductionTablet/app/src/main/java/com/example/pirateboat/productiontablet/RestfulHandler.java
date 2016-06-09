package com.example.pirateboat.productiontablet;

import android.os.AsyncTask;
import android.util.Log;

import com.example.pirateboat.productiontablet.data.OrderResult;
import com.example.pirateboat.productiontablet.data.getUpdates;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.nio.charset.Charset;

/**
 * Created by Swodah on 25-05-2016.
 * Class is used to create and use connections to the restful webservice
 */
public class RestfulHandler {
    InputStreamReader reader;
    private static final String TAG = "Production tablet";
    private URL url;
    OutputStream out;
    HttpURLConnection urlConnection = null;
    private static Gson gson = new GsonBuilder().create();
    public final Charset charset = Charset.forName("UTF-8");
    int attemptcounter;
    String baseurl = "http://10.176.160.175:8080";
    public int updatepoint = -1;
    /**
     * Constructer sets up the main url to be used, though it can be outsourced to the classes using it.
     */
    public RestfulHandler() throws MalformedURLException {
        Log.i(TAG, "resthandler created");

        if (url == null) {
            url = new URL(baseurl + "/RestService.svc/getAllActiveOrders");
        }

    }


    public URL getUrl() {
        return url;
    }

    public void setUrl(String url) {
        try {
            this.url = new URL(baseurl + url);
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }
    }

    /**
     *
     * @returns OrderResult after connecting to webservice and confirming if there are updates,
     * if no new updates have been made it returns null.
     */
    public OrderResult readStream() {
        InputStream is = null;
        OrderResult o = null;
        int check = -1;
        try {
            setUrl("/RestService.svc/getUpdates");
            urlConnection = (HttpURLConnection) url.openConnection();
            urlConnection.setReadTimeout(10000);
            urlConnection.setConnectTimeout(20000);
            urlConnection.setRequestMethod("GET");
            urlConnection.setDoInput(true);
            urlConnection.setDoOutput(false);
            urlConnection.connect();
            is = urlConnection.getInputStream();
            reader = new InputStreamReader(is, charset);
            getUpdates upDR = gson.fromJson(reader, getUpdates.class);
            check = upDR.getUpdatesResult;
            if (check > updatepoint) {


                setUrl("/RestService.svc/getAllActiveOrders");
                urlConnection = (HttpURLConnection) url.openConnection();
                urlConnection.setReadTimeout(10000);
                urlConnection.setConnectTimeout(20000);
                urlConnection.setRequestMethod("GET");
                urlConnection.setDoInput(true);
                urlConnection.setDoOutput(false);
                urlConnection.connect();
                is = urlConnection.getInputStream();
                reader = new InputStreamReader(is, charset);

                o = gson.fromJson(reader, OrderResult.class);
                updatepoint = upDR.getUpdatesResult;
                Log.i(TAG,"return object");
                return o;
            }

        } catch (Exception ioe) {
            ioe.printStackTrace();
        } finally {
            urlConnection.disconnect();


        }
        if (o == null && updatepoint<check) {
            if (attemptcounter < 3) {
                attemptcounter++;
                readStream();
            }
        }
        Log.i(TAG,"No new updates");
        return null;

    }

    /**
     *
     * @param output
     * Writes a String to the webservice, which is based on a JSON string, which is created based on a string
     */
    public void writeStream(String output) {

        try {
            urlConnection = (HttpURLConnection) url.openConnection();
            urlConnection.setReadTimeout(10000);
            urlConnection.setConnectTimeout(20000);
            urlConnection.setRequestMethod("POST");
            urlConnection.setDoInput(true);
            urlConnection.setDoOutput(true);
            urlConnection.connect();
            out = urlConnection.getOutputStream();

            String orJson = gson.toJson(output);

            out.write(orJson.getBytes(Charset.forName("UTF-8")));

            int response = urlConnection.getResponseCode();
            Log.d(TAG, "Response: " + response);
            out.flush();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            urlConnection.disconnect();
        }
    }
}