package com.example.pirateboat.productiontablet;

import android.app.Activity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.view.View;
import android.widget.Button;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TableRow.LayoutParams;
import android.widget.TextView;

import com.example.pirateboat.productiontablet.data.OrderResult;

import java.net.MalformedURLException;


public class OrderOverView extends Activity {
    TableLayout table_layout;

    OrderResult or;
    OrderResult storedOR;
    RestfulHandler rfh;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_order_over_view);

        table_layout = (TableLayout) findViewById(R.id.tableLayout1);
        try {
            rfh = new RestfulHandler();
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }
        new update().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
        ;

    }

    class update extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);


                or = rfh.readStream();

            if (or != null) {
                try {
                    if (or.getAllActiveOrdersResult.get(0).AltDeliveryInfo != null) {
                        int orderAmount = or.getAllActiveOrdersResult.size();
                        BuildTable(orderAmount, or);
                        storedOR = or;
                        or = null;
                    } else {
                        int orderAmount = storedOR.getAllActiveOrdersResult.size();
                        BuildTable(orderAmount, storedOR);
                    }
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
            try {
                Thread.sleep(12000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }

            doInBackground();
            return null;
        }


        protected void onPostExecute(Void param) {
        }
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
        final TableRow row = add;
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                table_layout.addView(row);
            }
        });
    }

    View.OnClickListener noteclicker(final Button button, final String ordername) {
        return new View.OnClickListener() {
            public void onClick(View v) {
                Intent myIntent = new Intent(OrderOverView.this, NotesView.class);
                myIntent.putExtra("SelectedON", ordername);
                startActivity(myIntent);
            }
        };
    }

    View.OnClickListener confirmationclicker(final Button button) {
        return new View.OnClickListener() {
            public void onClick(View v) {
                Intent myIntent = new Intent(OrderOverView.this, OrderConfirmation.class);
                myIntent.putExtra("SelectedON", button.getText());
                startActivity(myIntent);
            }
        };
    }

    private void BuildTable(int rows, final OrderResult data) {
        ClearTable();
        int cols = 13;
        // outer for loop
        for (int i = 0; i <= rows - 1; i++) {

            TableRow row = new TableRow(this);
            row.setLayoutParams(new TableLayout.LayoutParams(LayoutParams.MATCH_PARENT,
                    LayoutParams.WRAP_CONTENT));

            // inner for loop
            for (int j = 1; j <= cols; j++) {

                switch (j) {
                    case 1:
                        //knap
                        final Button btnON = new Button(this);
                        btnON.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        btnON.setBackgroundResource(R.drawable.cell_shape);
                        btnON.setPadding(40, 40, 40, 40);
                        btnON.setText(data.getAllActiveOrdersResult.get(i).OrderName);//hardcode knapper og links og labels
                        btnON.setId(btnON.generateViewId());
                        btnON.setOnClickListener(confirmationclicker(btnON));
                        row.addView(btnON);
                        break;
                    case 2:
//                        //textlinks
//                        TextView tl2 = new TextView(this);
//                        tl2.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
//                                LayoutParams.WRAP_CONTENT));
//                        tl2.setBackgroundResource(R.drawable.cell_shape);
//                        tl2.setPadding(40, 40, 40, 40);
//                        tl2.setId(tl2.generateViewId());
//                        tl2.setText("File links disabled");//hardcode knapper og links og labels
//                        row.addView(tl2);
                        break;
                    case 3:
                        //label textviews
                        TextView st4 = new TextView(this);
                        st4.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st4.setBackgroundResource(R.drawable.cell_shape);
                        st4.setPadding(40, 40, 40, 40);
                        st4.setId(st4.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station4 != null) {
                            st4.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station4.equals("Done") || data.getAllActiveOrdersResult.get(i).StationStatus.Station4.equals("Activte")) ? "X" : " ");
                        } else {
                            st4.setText("missing");
                        }
                        // st4.setText("missing");
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
                        st4d.setId(st4d.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station4 != null) {
                            st4d.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station4.equals("Done")) ? "X" : " ");
                        } else {
                            st4d.setText("missing");
                        }
                        //st4d.setText("missing");
                        row.addView(st4d);
                        break;
                    case 5:
                        //label textviews
                        TextView st5 = new TextView(this);
                        st5.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st5.setBackgroundResource(R.drawable.cell_shape);
                        st5.setPadding(40, 40, 40, 40);
                        st5.setId(st5.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station5 != null) {
                            st5.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station5.equals("Done") || data.getAllActiveOrdersResult.get(i).StationStatus.Station5.equals("Activte")) ? "X" : " ");
                        } else {
                            st5.setText("missing");
                        }
//                        st5.setText("missing");
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
                        st5d.setId(st5d.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station5 != null) {
                            st5d.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station5.equals("Done")) ? "X" : " ");
                        } else {
                            st5d.setText("missing");
                        }
//                        st5d.setText("missing");
                        row.addView(st5d);
                        break;
                    case 7:
                        //label textviews
                        TextView st6 = new TextView(this);
                        st6.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st6.setBackgroundResource(R.drawable.cell_shape);
                        st6.setPadding(40, 40, 40, 40);
                        st6.setId(st6.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station6 != null) {
                            st6.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station6.equals("Done") || data.getAllActiveOrdersResult.get(i).StationStatus.Station6.equals("Activte")) ? "X" : " ");
                        } else {
                            st6.setText("missing");
                        }
                        //st6.setText("missing");
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
                        st6d.setId(st6d.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station6 != null) {
                            st6d.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station6.equals("Done")) ? "X" : " ");
                        } else {
                            st6d.setText("missing");
                        }
                        //st6d.setText("missing");
                        row.addView(st6d);
                        break;
                    case 9:
                        //label textviews
                        TextView st7 = new TextView(this);
                        st7.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st7.setBackgroundResource(R.drawable.cell_shape);
                        st7.setPadding(40, 40, 40, 40);
                        st7.setId(st7.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station7 != null) {
                            st7.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station7.equals("Done") || data.getAllActiveOrdersResult.get(i).StationStatus.Station7.equals("Activte")) ? "X" : " ");
                        } else {
                            st7.setText("missing");
                        }
                        //st7.setText("missing");
                        //hardcode knapper og links og labels
                        row.addView(st7);
                        break;
                    case 10:
                        TextView st7d = new TextView(this);
                        st7d.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st7d.setBackgroundResource(R.drawable.cell_shape);
                        st7d.setPadding(40, 40, 40, 40);
                        st7d.setId(st7d.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station7 != null) {
                            st7d.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station7.equals("Done")) ? "X" : " ");
                        } else {
                            st7d.setText("missing");
                        }
//                        st7d.setText("missing");
                        row.addView(st7d);
                        break;
                    case 11:
                        //label textviews
                        TextView st8 = new TextView(this);
                        st8.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st8.setBackgroundResource(R.drawable.cell_shape);
                        st8.setPadding(40, 40, 40, 40);
                        st8.setId(st8.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station8 != null) {
                            st8.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station8.equals("Done") || data.getAllActiveOrdersResult.get(i).StationStatus.Station8.equals("Activte")) ? "X" : " ");
                        } else {
                            st8.setText("missing");
                        }
//                        st8.setText("missing");
                        //hardcode knapper og links og labels
                        row.addView(st8);
                        break;
                    case 12:
                        TextView st8d = new TextView(this);
                        st8d.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        st8d.setBackgroundResource(R.drawable.cell_shape);
                        st8d.setPadding(40, 40, 40, 40);
                        st8d.setId(st8d.generateViewId());
                        if (data.getAllActiveOrdersResult.get(i).StationStatus.Station8 != null) {
                            st8d.setText((data.getAllActiveOrdersResult.get(i).StationStatus.Station8.equals("Done")) ? "X" : " ");
                        } else {
                            st8d.setText("missing");
                        }
//                        st8d.setText("missing");
                        row.addView(st8d);
                        break;
                    case 13:
                        //button
                        final String ordername = data.getAllActiveOrdersResult.get(i).OrderName;
                        Button btn = new Button(this);
                        btn.setLayoutParams(new LayoutParams(LayoutParams.WRAP_CONTENT,
                                LayoutParams.WRAP_CONTENT));
                        btn.setBackgroundResource(R.drawable.cell_shape);
                        btn.setPadding(5, 5, 5, 5);
                        btn.setId(btn.generateViewId());
                        btn.setOnClickListener(noteclicker(btn, ordername));

                        btn.setText("Notes");
                        row.addView(btn);
                        break;

                }


            }
            addRow(row);


        }
    }
}
