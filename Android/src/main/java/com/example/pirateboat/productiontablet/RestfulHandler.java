package com.example.pirateboat.productiontablet;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import org.json.JSONObject;
import org.json.JSONException;
/**
 * Created by Swodah on 25-05-2016.
 */
public class RestfulHandler {
    private URL url;
    InputStream in;
    OutputStream out;
    HttpURLConnection urlConnection = null;
    String response;
    JSONObject obj;
    private String outputmessage;

    public RestfulHandler() throws MalformedURLException {
        if(url == null){
            url = new URL("http://www.android.com/");
        }


        try {
            urlConnection = (HttpURLConnection) url.openConnection();
            urlConnection.setReadTimeout(10000);
            urlConnection.setConnectTimeout(15000);
            urlConnection.setRequestMethod("POST");
            urlConnection.setDoInput(true);
            urlConnection.setDoOutput(true);
            urlConnection.setChunkedStreamingMode(0);

        } catch (IOException e) {
            e.printStackTrace();
        }
        try {
           in = new BufferedInputStream(urlConnection.getInputStream());
            try {
                writeStream("update");
              readStream();
            } finally {
                urlConnection.disconnect();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
    public String getOutputmessage() {
        return outputmessage;
    }

    public void setOutputmessage(String outputmessage) {
        this.outputmessage = outputmessage;
    }

    public URL getUrl() {
        return url;
    }

    public void setUrl(URL url) {
        this.url = url;
    }


    private void readStream() {

        try {
            in = new BufferedInputStream(urlConnection.getInputStream());
            //obj = new JSONObject(in.read());

            //response = in.read();
        }catch(IOException e){
        }
        finally {
            urlConnection.disconnect();
        }
    }


    private void writeStream(String output) {
        outputmessage=output;
        try {

            urlConnection = (HttpURLConnection) url.openConnection();
            out.write(outputmessage.getBytes());
            out.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}