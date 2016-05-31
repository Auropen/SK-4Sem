package com.example.pirateboat.productiontablet;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;

import java.util.ArrayList;

public class NotesView extends Activity {
    ListView lw;
    Button postbtn;
    EditText et;
    ArrayList<String> comments = new ArrayList<String>();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout .activity_notes_viewnew);
        lw = (ListView) findViewById(R.id.mobile_list);
        et = (EditText) findViewById(R.id.commentField);
        postbtn = (Button) findViewById(R.id.postBTN);
        postbtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                postComment(et.getText().toString());
            }
        });
        postbtn.setText("Post Comment");

        comments.add("moved hinge 2'' down");
        comments.add("Out of brown color");
        comments.add("Used 4mm screws instead of 3.5mm");
        updateComments(comments);
    }

    public void updateComments(ArrayList<String> comments){

        ArrayAdapter adapter = new ArrayAdapter<String>(this, R.layout.activity_listview, comments);
        lw.setAdapter(adapter);

    }
    public void postComment(String text){
        comments.add(text);
        et.setText("");
        updateComments(comments);
    }
}
