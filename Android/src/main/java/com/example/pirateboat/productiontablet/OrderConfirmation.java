package com.example.pirateboat.productiontablet;

import android.app.Activity;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.StrictMode;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TableLayout;
import android.widget.TableRow;
import android.widget.TextView;

import com.example.pirateboat.productiontablet.data.Order;
import com.example.pirateboat.productiontablet.data.OrderResult;

import java.net.MalformedURLException;
import java.util.ArrayList;

public class OrderConfirmation extends Activity {
    TableLayout table_layoutoc;
    OrderResult or;
    Bundle bundle;
    String message;
    Order storedOR;
    Order order;
    RestfulHandler rfh2;
    ArrayList<Integer> hidelist = new ArrayList<Integer>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_order_confirmation);


        table_layoutoc = (TableLayout) findViewById(R.id.tableLayout2);

        bundle = getIntent().getExtras();
        message = bundle.getString("SelectedON");
        Log.i("OC", message);
        try {
            rfh2 = new RestfulHandler();
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }
        new update2().executeOnExecutor(AsyncTask.THREAD_POOL_EXECUTOR);
    }

    class update2 extends AsyncTask<Void, Void, Void> {

        @Override
        protected Void doInBackground(Void... params) {
            Log.i("OC", "test");
            StrictMode.setThreadPolicy(StrictMode.ThreadPolicy.LAX);


                or = rfh2.readStream();

            if (or != null) {
                Log.i("OC", "Do in background");
                for (int i = 0; i < or.getAllActiveOrdersResult.size(); i++) {
                    if (or.getAllActiveOrdersResult.get(i).OrderName.equals(message)) {
                        order = or.getAllActiveOrdersResult.get(i);
                    }
                }

                try {
                    if (order.OrderName != null) {
                        BuildTable(order);
                        storedOR = order;
                        or = null;

                    } else {
                        BuildTable(storedOR);
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
            // doInBackground();
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
                table_layoutoc.removeAllViews();
            }
        });
    }

    public void addRow(TableRow add) {
        final TableRow row = add;
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                table_layoutoc.addView(row);
            }
        });
    }

    View.OnClickListener toggleColumns(final Button button) {
        return new View.OnClickListener() {
            public void onClick(View v) {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        for (int i = 0; i < hidelist.size(); i++) {
                            if (findViewById(hidelist.get(i)).getVisibility() == View.GONE) {
                                findViewById(hidelist.get(i)).setVisibility(View.VISIBLE);
                            } else {
                                findViewById(hidelist.get(i)).setVisibility(View.GONE);

                            }
                        }

                        if (findViewById(hidelist.get(0)).getVisibility() == View.GONE) {
                            button.setText("show");
                        } else {
                            button.setText("Hide");
                        }
                    }
                });
            }
        };
    }

    View.OnClickListener noteclicker(final Button button, final String ordername) {
        return new View.OnClickListener() {
            public void onClick(View v) {
                Intent myIntent = new Intent(OrderConfirmation.this, NotesView.class);
                myIntent.putExtra("SelectedON", ordername);
                startActivity(myIntent);
            }
        };
    }


    View.OnClickListener updateStation(final Button button, final int category, final int element, final int number) {
        return new View.OnClickListener() {
            public void onClick(View v) {
                if (order.Categories.get(category).Elements.get(element).StationStatus[number] == true) {
                    order.Categories.get(category).Elements.get(element).StationStatus[number] = false;
                } else {
                    order.Categories.get(category).Elements.get(element).StationStatus[number] = true;
                }
                //send or to webserver
            }
        };
    }

    private void BuildTable(Order data) {
        ClearTable();
        for (int k = 0; k < data.Categories.size(); k++) {
            TableRow CategoryRow = new TableRow(this);

            CategoryRow.setLayoutParams(new TableLayout.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));
            TextView CategoryName = new TextView(this);
            TableRow.LayoutParams params = new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                    TableRow.LayoutParams.WRAP_CONTENT);
            params.span = 2;

            CategoryName.setLayoutParams(params);

            CategoryName.setBackgroundResource(R.drawable.cell_shape);
            CategoryName.setPadding(40, 40, 40, 40);
            CategoryName.setId(CategoryName.generateViewId());
            CategoryName.setText(data.Categories.get(k).Name);

            CategoryRow.addView(CategoryName);

            final Button showSpecieal = new Button(this);
            showSpecieal.setLayoutParams(params);
            showSpecieal.setBackgroundResource(R.drawable.cell_shape);
            showSpecieal.setPadding(40, 40, 40, 40);
            showSpecieal.setText("Hide");//hardcode knapper og links og labels
            showSpecieal.setId(showSpecieal.generateViewId());
            showSpecieal.setOnClickListener(toggleColumns(showSpecieal));
            CategoryRow.addView(showSpecieal);

            addRow(CategoryRow);

            int rows = data.Categories.get(k).Elements.size();
            int cols = 12;
            Log.i("OC", "category added");
            // outer for loop
            for (int i = 0; i <= rows - 1; i++) {

                TableRow rowinfo = new TableRow(this);
                rowinfo.setLayoutParams(new TableLayout.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                        TableRow.LayoutParams.WRAP_CONTENT));
                TableRow row = new TableRow(this);
                row.setLayoutParams(new TableLayout.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                        TableRow.LayoutParams.WRAP_CONTENT));
                Log.i("OC", "new row");
                // inner for loop
                for (int j = 1; j <= cols; j++) {

                    switch (j) {
                        case 1:
                            //Element info
                            TextView info = new TextView(this);
                            info.setVisibility(View.GONE);
                            TableRow.LayoutParams paramsInfo = new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT);

                            paramsInfo.span = 11;
//                            new TableLayout.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT, TableRow.LayoutParams.WRAP_CONTENT, 0.40f)
                            info.setLayoutParams(paramsInfo);
                            info.setBackgroundResource(R.drawable.cell_shape);
                            info.setPadding(40, 40, 40, 40);
                            info.setId(info.generateViewId());

                            String elementinfo = "";
                            for (int n = 0; n < data.Categories.get(k).Elements.get(i).ElementInfo.size(); n++) {
                                elementinfo += data.Categories.get(k).Elements.get(i).ElementInfo.get(n) + "\n";
                            }

                            info.setText(elementinfo);
                            rowinfo.addView(info);
                            addRow(rowinfo);


                            break;
                        case 2:

                            //pos


                            TextView pos = new TextView(this);
                            TableRow.LayoutParams paramsPos = new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT);
                            paramsPos.weight = 0.10f;
                            pos.setLayoutParams(paramsPos);
                            pos.setBackgroundResource(R.drawable.cell_shape);
                            pos.setPadding(40, 40, 40, 40);
                            pos.setId(pos.generateViewId());
                            pos.setText(data.Categories.get(k).Elements.get(i).Position);//hardcode knapper og links og labels
                            row.addView(pos);


                            break;
                        case 3:
                            //hinge
                            TextView hinge = new TextView(this);
                            hinge.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            hinge.setBackgroundResource(R.drawable.cell_shape);
                            hinge.setPadding(40, 40, 40, 40);
                            hinge.setId(hinge.generateViewId());
                            hinge.setText(data.Categories.get(k).Elements.get(i).Hinge);//hardcode knapper og links og labels
                            row.addView(hinge);


                            break;
                        case 4:
                            //finish
                            TextView finish = new TextView(this);
                            finish.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            finish.setBackgroundResource(R.drawable.cell_shape);
                            finish.setPadding(40, 40, 40, 40);
                            finish.setId(finish.generateViewId());
                            finish.setText(data.Categories.get(k).Elements.get(i).Finish);//hardcode knapper og links og labels
                            row.addView(finish);

                            break;
                        case 5:
                            //Amount
                            TextView amount = new TextView(this);
                            amount.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            amount.setBackgroundResource(R.drawable.cell_shape);
                            amount.setPadding(40, 40, 40, 40);
                            amount.setId(amount.generateViewId());
                            String amountnr = data.Categories.get(k).Elements.get(i).Amount;
                            amount.setText(amountnr.substring(0, amountnr.indexOf(".") + 2));//hardcode knapper og links og labels
                            row.addView(amount);

//                            addRow(row);
//
//                            row = new TableRow(this);
//                            row.setLayoutParams(new TableLayout.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
//                                    TableRow.LayoutParams.WRAP_CONTENT));
                            break;
                        case 6:
                            //Unit
                            TextView unit = new TextView(this);
                            unit.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            unit.setBackgroundResource(R.drawable.cell_shape);
                            unit.setPadding(40, 40, 40, 40);
                            unit.setId(unit.generateViewId());
                            unit.setText(data.Categories.get(k).Elements.get(i).Unit);//hardcode knapper og links og labels
                            row.addView(unit);


                            break;

                        case 7:
                            //knap
                            final Button st4 = new Button(this);
                            st4.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            st4.setBackgroundResource(R.drawable.cell_shape);
                            st4.setPadding(40, 40, 40, 40);
                            st4.setText("sta4");//hardcode knapper og links og labels
                            st4.setId(st4.generateViewId());
                            hidelist.add(st4.getId());
                            st4.setOnClickListener(updateStation(st4, k, i, j));
                            row.addView(st4);
                            break;
                        case 8:
                            //knap
                            final Button st5 = new Button(this);
                            st5.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            st5.setBackgroundResource(R.drawable.cell_shape);
                            st5.setPadding(40, 40, 40, 40);
                            st5.setText("Sta5");//hardcode knapper og links og labels
                            st5.setId(st5.generateViewId());
                            hidelist.add(st5.getId());
                            st5.setOnClickListener(updateStation(st5, k, i, j));
                            row.addView(st5);
                            break;
                        case 9:
                            //knap
                            final Button st6 = new Button(this);
                            st6.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            st6.setBackgroundResource(R.drawable.cell_shape);
                            st6.setPadding(40, 40, 40, 40);
                            st6.setText("Sta6");//hardcode knapper og links og labels
                            st6.setId(st6.generateViewId());
                            hidelist.add(st6.getId());
                            st6.setOnClickListener(updateStation(st6, k, i, j));
                            row.addView(st6);
                            break;
                        case 10:
                            //knap
                            final Button st7 = new Button(this);
                            st7.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            st7.setBackgroundResource(R.drawable.cell_shape);
                            st7.setPadding(40, 40, 40, 40);
                            st7.setText("Sta7");//hardcode knapper og links og labels
                            st7.setId(st7.generateViewId());
                            hidelist.add(st7.getId());
                            st7.setOnClickListener(updateStation(st7, k, i, j));
                            row.addView(st7);
                            break;
                        case 11:
                            //knap
                            final Button st8 = new Button(this);
                            st8.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            st8.setBackgroundResource(R.drawable.cell_shape);
                            st8.setPadding(40, 40, 40, 40);
                            st8.setText("Sta8");//hardcode knapper og links og labels
                            st8.setId(st8.generateViewId());
                            hidelist.add(st8.getId());
                            st8.setOnClickListener(updateStation(st8, k, i, j));
                            row.addView(st8);
                            break;
                        case 12:
                            //button
                            final String ordername = data.OrderName;
                            Button btn = new Button(this);
                            btn.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                    TableRow.LayoutParams.WRAP_CONTENT));
                            btn.setBackgroundResource(R.drawable.cell_shape);
                            btn.setPadding(5, 5, 5, 5);
                            btn.setId(btn.generateViewId());
                            hidelist.add(btn.getId());
                            btn.setOnClickListener(noteclicker(btn, ordername));

                            btn.setText("Notes");//hardcode knapper og links og labels
                            row.addView(btn);
                            Log.i("OC", "create button notes");
                            break;

                    }


                }


                addRow(row);
            }
        }
    }
}
