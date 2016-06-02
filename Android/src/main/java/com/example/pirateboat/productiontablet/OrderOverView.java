package com.example.pirateboat.productiontablet;

import android.app.Activity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.TableRow.LayoutParams;
import android.widget.TextView;

import com.example.pirateboat.productiontablet.data.OrderResult;

import java.net.MalformedURLException;
import java.security.spec.ECField;
import java.util.Timer;


public class OrderOverView extends Activity {
    TableLayout table_layout;
    Button btn;
    OrderResult or;
    OrderResult storedOR;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_order_over_view);

        table_layout = (TableLayout) findViewById(R.id.tableLayout1);

        new update().execute();

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
            try {
                if (or.getOrderResult.AltDeliveryInfo != null) {
                    BuildTable(5, or);
                    storedOR = or;
                }
            }catch(Exception e){
                e.printStackTrace();
            }
            or = null;
            try {
                Thread.sleep(120000);
            }
            catch (InterruptedException e)
            {
                e.printStackTrace();
            }
            doInBackground();
            return null;
        }


        protected void onPostExecute(Void param) { }
     // AsyncTask over
    }

    public void ClearTable() {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                table_layout.removeAllViews();
            }
        });
    }
    public void addRow(TableRow add) {
        final TableRow row =add;
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                table_layout.addView(row);
            }
        });
    }
    private void BuildTable(int rows,OrderResult data) {
        ClearTable();
        int cols = 13;
        // outer for loop
        for (int i = 1; i <= rows; i++) {

            TableRow row = new TableRow(this);
            row.setLayoutParams(new TableLayout.LayoutParams(LayoutParams.MATCH_PARENT,
                    LayoutParams.WRAP_CONTENT));

            // inner for loop
            for (int j = 1; j <= cols; j++) {

                switch (j){
                    case 1:
                        //knap
                        final Button btnON = new Button(this);
                        btnON.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        btnON.setBackgroundResource(R.drawable.cell_shape);
                        btnON.setPadding(40, 40, 40, 40);
                        btnON.setText(data.getOrderResult.OrderName);//hardcode knapper og links og labels
                        btnON.setOnClickListener(new OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                Intent myIntent = new Intent(OrderOverView.this,OrderConfirmation.class);
                                myIntent.putExtra("message", btnON.getText());
                                startActivity(myIntent);
                            }
                        });
                        row.addView(btnON);
                        break;
                    case 2:
                        //textlinks
                        TextView tl2 = new TextView(this);
                        tl2.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        tl2.setBackgroundResource(R.drawable.cell_shape);
                        tl2.setPadding(40, 40, 40, 40);
                        tl2.setText("File links disabled");//hardcode knapper og links og labels
                        row.addView(tl2);
                        break;
                    case 3:
                        //label textviews
                        TextView st4 = new TextView(this);
                        st4.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st4.setBackgroundResource(R.drawable.cell_shape);
                        st4.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station4.matches("wip")||data.getOrderResult.orderStatuses.Station4.matches("done") ){
//                            st4.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station4.matches("notstarted")){
//                            st4.setText(" ");
//                        }else{
//                            st4.setText("missing");
//                        }
                        st4.setText("missing");
                        //hardcode knapper og links og labels
                        row.addView(st4);
                        break;
                    case 4:
                        //label textviews
                        TextView st4d = new TextView(this);
                        st4d.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st4d.setBackgroundResource(R.drawable.cell_shape);
                        st4d.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station4.matches("done") ){
//                            st4d.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station4.matches("notstarted")){
//                            st4d.setText(" ");
//                        }else{
//                            st4d.setText("missing");
//                        }
                        st4d.setText("missing");
                        row.addView(st4d);
                        break;
                    case 5:
                        //label textviews
                        TextView st5 = new TextView(this);
                        st5.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st5.setBackgroundResource(R.drawable.cell_shape);
                        st5.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station5.matches("wip")||data.getOrderResult.orderStatuses.Station5.matches("done") ){
//                            st5.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station5.matches("notstarted")){
//                            st5.setText(" ");
//                        }else{
//                            st5.setText("missing");
//                        }
                        st5.setText("missing");
                        //hardcode knapper og links og labels
                        row.addView(st5);
                        break;
                    case 6:
                        //label textviews
                        TextView st5d = new TextView(this);
                        st5d.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st5d.setBackgroundResource(R.drawable.cell_shape);
                        st5d.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station5.matches("done") ){
//                            st5d.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station5.matches("notstarted")){
//                            st5d.setText(" ");
//                        }else{
//                            st5d.setText("missing");
//                        }
                        st5d.setText("missing");
                        row.addView(st5d);
                        break;
                    case 7:
                        //label textviews
                        TextView st6 = new TextView(this);
                        st6.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st6.setBackgroundResource(R.drawable.cell_shape);
                        st6.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station6.matches("wip")||data.getOrderResult.orderStatuses.Station6.matches("done") ){
//                            st6.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station6.matches("notstarted")){
//                            st6.setText(" ");
//                        }else{
//                            st6.setText("missing");
//                        }
                        st6.setText("missing");
                        //hardcode knapper og links og labels
                        row.addView(st6);
                        break;
                    case 8:
                        //label textviews
                        TextView st6d = new TextView(this);
                        st6d.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st6d.setBackgroundResource(R.drawable.cell_shape);
                        st6d.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station6.matches("done") ){
//                            st6d.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station6.matches("notstarted")){
//                            st6d.setText(" ");
//                        }else{
//                            st6d.setText("missing");
//                        }
                        st6d.setText("missing");
                        row.addView(st6d);
                        break;
                    case 9:
                        //label textviews
                        TextView st7 = new TextView(this);
                        st7.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st7.setBackgroundResource(R.drawable.cell_shape);
                        st7.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station7.matches("wip")||data.getOrderResult.orderStatuses.Station7.matches("done") ){
//                            st7.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station7.matches("notstarted")){
//                            st7.setText(" ");
//                        }else{
//                            st7.setText("missing");
//                        }
                        st7.setText("missing");
                        //hardcode knapper og links og labels
                        row.addView(st7);
                        break;
                    case 10:
                        TextView st7d = new TextView(this);
                        st7d.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st7d.setBackgroundResource(R.drawable.cell_shape);
                        st7d.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station7.matches("done") ){
//                            st7d.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station7.matches("notstarted")){
//                            st7d.setText(" ");
//                        }else{
//                            st7d.setText("missing");
//                        }
                        st7d.setText("missing");
                        row.addView(st7d);
                        break;
                    case 11:
                        //label textviews
                        TextView st8 = new TextView(this);
                        st8.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st8.setBackgroundResource(R.drawable.cell_shape);
                        st8.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station8.matches("wip")||data.getOrderResult.orderStatuses.Station8.matches("done") ){
//                            st8.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station8.matches("notstarted")){
//                            st8.setText(" ");
//                        }else{
//                            st8.setText("missing");
//                        }
                        st8.setText("missing");
                        //hardcode knapper og links og labels
                        row.addView(st8);
                        break;
                    case 12:
                        TextView st8d = new TextView(this);
                        st8d.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st8d.setBackgroundResource(R.drawable.cell_shape);
                        st8d.setPadding(40, 40, 40, 40);
//                        if(data.getOrderResult.orderStatuses.Station8.matches("done") ){
//                            st8d.setText("X");
//                            //done
//                            //wip
//                            //notstarted
//                        }else if(data.getOrderResult.orderStatuses.Station8.matches("notstarted")){
//                            st8d.setText(" ");
//                        }else{
//                            st8d.setText("missing");
//                        }
                        st8d.setText("missing");
                        row.addView(st8d);
                        break;
                    case 13:
                        //button
                        Button btn = new Button(this);
                        btn.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        btn.setBackgroundResource(R.drawable.cell_shape);
                        btn.setPadding(5, 5, 5, 5);
                        btn.setOnClickListener(new OnClickListener() {
                            @Override
                            public void onClick(View v) {
                                Intent myIntent = new Intent(OrderOverView.this,NotesView.class);
                                startActivity(myIntent);
                            }
                        });

                        btn.setText("Notes");//hardcode knapper og links og labels
                        row.addView(btn);
                        break;

                }




            }
            addRow(row);
//            table_layout.addView(row);

        }
    }
}
