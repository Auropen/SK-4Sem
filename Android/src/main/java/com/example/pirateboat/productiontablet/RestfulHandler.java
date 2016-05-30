package com.example.pirateboat.productiontablet;

import android.os.AsyncTask;

import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.nio.charset.Charset;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.reflect.TypeToken;
/**
 * Created by Swodah on 25-05-2016.
 */
public class RestfulHandler extends AsyncTask<Void, Void, Void> {
    private URL url;
    InputStream in;
    OutputStream out;
    HttpURLConnection urlConnection = null;
    String response;
    JSONObject obj;
    private String outputmessage;
    public final Charset charset = Charset.forName("UTF-8");

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
            urlConnection.connect();
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

    @Override
    protected Void doInBackground(Void... params) {
        return null;
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


//    // Given a URL, establishes an HttpUrlConnection and retrieves
//// the web page content as a InputStream, which it returns as
//// a string.
//    public List<Overview> downloadUrl(String myurl) throws IOException {
//        InputStream is = null;
//        // Only display the first 500 characters of the retrieved
//        // web page content.
//        int len = 500;
//
//        try {
//            Uri uri = Uri.parse(endpoint);
//            Log.d(TAG, "Querying " + uri.toString());
//            URL url = new URL(uri.buildUpon().appendPath(username).appendPath("questionnaires").build().toString());
//            Log.d(TAG, "Querying " + url.toString());
//            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
//            conn.setReadTimeout(10000 /* milliseconds */);
//            conn.setConnectTimeout(15000 /* milliseconds */);
//            conn.setRequestMethod("GET");
//            conn.setDoInput(true);
//            // Starts the query
//            conn.connect();
//            int response = conn.getResponseCode();
//            Log.d(TAG, "The response is: " + response);
//            is = conn.getInputStream();
//
//            // Convert the InputStream into a string
//            InputStreamReader reader = new InputStreamReader(is, charset);
//            return gson.fromJson(reader, new TypeToken<List<Questionnaire>>() {}.getType());
//
//            // Makes sure that the InputStream is closed after the app is
//            // finished using it.
//        } finally {
//            if (is != null) {
//                is.close();
//            }
//        }
//    }

    private void readStream() {
        InputStream is = null;
        try {
            urlConnection.setRequestMethod("POST");
            urlConnection.connect();
            in = urlConnection.getInputStream();
            is = urlConnection.getInputStream();
            InputStreamReader reader = new InputStreamReader(is, charset);
            obj = new JSONObject(in.toString());
            reader.read();
            //response =
        }catch(Exception ioe){
        }
        finally {
            urlConnection.disconnect();

        }
    }


    private void writeStream(String output) {
        outputmessage=output;
        try {
            urlConnection.connect();
            urlConnection = (HttpURLConnection) url.openConnection();
            out.write(outputmessage.getBytes());
            out.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }finally {
            urlConnection.disconnect();
        }
    }
}