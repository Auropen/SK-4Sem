package com.example.pirateboat.productiontablet;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;

import com.example.pirateboat.productiontablet.data.Order;
import com.example.pirateboat.productiontablet.data.OrderResult;

import java.net.MalformedURLException;
import java.util.ArrayList;

public class NotesView extends Activity {
    ListView lw;
    Button postbtn;
    EditText et;
    String message;
    OrderResult or;
    Order order;
    RestfulHandler rfh;
    ArrayList<String> comments = new ArrayList<String>();
    ArrayList<String> newcomments = new ArrayList<String>();

    /**
     * Method used when the activity is created, contains the functions for retrieving or and
     * building table call
     * @param savedInstanceState
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_notes_viewnew);
        lw = (ListView) findViewById(R.id.mobile_list);
        et = (EditText) findViewById(R.id.commentField);
        postbtn = (Button) findViewById(R.id.postBTN);
        postbtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                postComment(et.getText().toString());
            }
        });
        Bundle bundle = getIntent().getExtras();
        message = bundle.getString("SelectedON");
        postbtn.setText("Post Comment");

        StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
        try {
            rfh = new RestfulHandler();
            or = rfh.readStream();
        } catch (MalformedURLException e) {
            e.printStackTrace();

        }
        for (int i = 0; i < or.getAllActiveOrdersResult.size(); i++) {
            if (or.getAllActiveOrdersResult.get(i).OrderName.equals(message)) {
                order = or.getAllActiveOrdersResult.get(i);
            }
        }
        comments.clear();
        for (int i = 0; i < order.Notes.size(); i++) {
            comments.add(order.Notes.get(i).toString());
        }
        updateComments(comments);

//        new update3().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);

    }
/**
 * not yet implemented update function which is almost ready, needs to be changed to fit newer code
 */
//    class update3 extends AsyncTask<Void, Void, Void> {
//
//
//
//        @Override
//        protected Void doInBackground(Void... params) {
//            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
//            try {
//                RestfulHandler rfh = new RestfulHandler();
//                or = rfh.readStream();
//            } catch (MalformedURLException e) {
//                e.printStackTrace();
//
//            }
//            for(int i = 0; i<or.getAllActiveOrdersResult.size();i++){
//                if(or.getAllActiveOrdersResult.get(i).OrderName.equals(message)){
//                    order=or.getAllActiveOrdersResult.get(i);
//                }
//            }
//                comments.clear();
//            for(int i = 0; i<order.Notes.size();i++) {
//                comments.add(order.Notes.get(i).toString());
//            }
//            updateComments(comments);
//            try {
//                Thread.sleep(12000);
//            } catch (InterruptedException e) {
//                e.printStackTrace();
//            }
//
//            doInBackground();
//            return null;
//        }
//        protected void onPostExecute(Void param) { }
//        // AsyncTask over
//
//    }

    /**
     * takes the array of comments and adds it to the listview
     * @param comments
     */
    public void updateComments(ArrayList<String> comments) {

        ArrayAdapter adapter = new ArrayAdapter<String>(this, R.layout.activity_listview, comments);
        lw.setAdapter(adapter);

    }

    /**
     * takes the comment from the textfield and adds it to the outgoing comment list and the
     * update comment list after which update and upload comments are called
     * @param text
     */
    public void postComment(String text) {
        newcomments.add(text);
        comments.add(text);
        et.setText("");
        updateComments(comments);
        uploadComments();

    }

    /**
     * uses the resthandler to contact the service and post the comments from the list, then
     * clears the list of newcomments
     */
    public void uploadComments() {
        rfh.setUrl("/RestService.svc/addNote");
        for (int i = 0; i < newcomments.size(); i++) {
            Log.i("Notes",order.OrderNumber + ";" + newcomments.get(i));
            rfh.writeStream(order.OrderNumber + ";" + newcomments.get(i));
        }
        newcomments.clear();

        rfh.setUrl("/RestService.svc/getAllActiveOrders");
    }
}
