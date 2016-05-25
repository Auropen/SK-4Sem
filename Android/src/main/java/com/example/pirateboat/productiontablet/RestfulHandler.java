package com.example.pirateboat.productiontablet;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

/**
 * Created by Swodah on 25-05-2016.
 */
public class RestfulHandler {
    URL url;
    InputStream in;
    public RestfulHandler() throws MalformedURLException {
        url = new URL("http://www.android.com/");
        HttpURLConnection urlConnection = null;
        try {
            urlConnection = (HttpURLConnection) url.openConnection();
        } catch (IOException e) {
            e.printStackTrace();
        }
        try {
           in = new BufferedInputStream(urlConnection.getInputStream());
            try {
                readStream(in);
            } finally {
                urlConnection.disconnect();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void readStream(InputStream in) {

    }
    private void sendStream() {
        HttpURLConnection urlConnection = null;
        try {
            urlConnection = (HttpURLConnection) url.openConnection();
        } catch (IOException e) {
            e.printStackTrace();
        }
        try {
            urlConnection.setDoOutput(true);
            urlConnection.setChunkedStreamingMode(0);

            OutputStream out = new BufferedOutputStream(urlConnection.getOutputStream());
            writeStream(out);

            in = new BufferedInputStream(urlConnection.getInputStream());
        }
            catch(IOException e){
                e.printStackTrace();
            }
            try{
            readStream(in);
            }
            finally {
                urlConnection.disconnect();
            }
        }

    private void writeStream(OutputStream out) {

    }
}