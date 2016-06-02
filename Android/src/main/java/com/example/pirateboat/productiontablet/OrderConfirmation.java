package com.example.pirateboat.productiontablet;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.widget.Button;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import com.example.pirateboat.productiontablet.data.OrderResult;

import java.net.MalformedURLException;
import java.util.ArrayList;

public class OrderConfirmation extends Activity {
    TableLayout table_layout;
    OrderResult or;
    Bundle bundle;
    String message;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_order_confirmation);


        table_layout = (TableLayout) findViewById(R.id.tableLayout1);
        ArrayList<String> data = new ArrayList<String>();
        bundle = getIntent().getExtras();
        message = bundle.getString("message");
    }
    class update extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);
            try {
                RestfulHandler rfh = new RestfulHandler();
                or = rfh.readStream();
            } catch (MalformedURLException e) {
                e.printStackTrace();

            }


            BuildTable(13,or);

            //doInBackground();
            return null;
        }


        protected void onPostExecute(Void param) { }
        // AsyncTask over
    }
    private void BuildTable(int rows,OrderResult data) {

        int cols = 13;
        // outer for loop
        for (int i = 1; i <= rows; i++) {

            TableRow row = new TableRow(this);
            row.setLayoutParams(new TableLayout.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));

            // inner for loop
            for (int j = 1; j <= cols; j++) {

                switch (j){
                    case 1:
                        //knap
                        break;
                    case 2:
                        //textlinks
                        TextView tl = new TextView(this);
                        tl.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        tl.setBackgroundResource(R.drawable.cell_shape);
                        tl.setPadding(40, 40, 40, 40);
                        tl.setText("R " + i + ", C" + j);//hardcode knapper og links og labels
                        row.addView(tl);
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        //label textviews
                        TextView tv = new TextView(this);
                        tv.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        tv.setBackgroundResource(R.drawable.cell_shape);
                        tv.setPadding(40, 40, 40, 40);
                        tv.setText("X");//hardcode knapper og links og labels
                        row.addView(tv);
                        break;
                    case 13:
                        //button
                        Button btn = new Button(this);
                        btn.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        btn.setBackgroundResource(R.drawable.cell_shape);
                        btn.setPadding(5, 5, 5, 5);

                        btn.setText("Notes");//hardcode knapper og links og labels
                        row.addView(btn);
                        break;

                }




            }


            table_layout.addView(row);

        }
    }
}
