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
import android.widget.TextView;

import com.example.pirateboat.productiontablet.data.Order;
import com.example.pirateboat.productiontablet.data.OrderResult;

import java.net.MalformedURLException;
import java.util.ArrayList;

public class OrderConfirmation extends Activity {
    TableLayout table_layout;
    OrderResult or;
    Bundle bundle;
    String message;
    Order storedOR;
    Order order;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_order_confirmation);


        table_layout = (TableLayout) findViewById(R.id.tableLayout1);

        bundle = getIntent().getExtras();
        message = bundle.getString("SelectedON");
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
            for(int i = 0; i<or.getAllActiveOrdersResult.size();i++){
                if(or.getAllActiveOrdersResult.get(i).OrderName.equals(message)){
                    order=or.getAllActiveOrdersResult.get(i);
                }
            }
            try {
                if (order.OrderName!=null) {
                    BuildTable(5, order);
                     storedOR = order;
                    or = null;

                }else{
                    BuildTable(13,storedOR);
                }
            }catch(Exception e){
                e.printStackTrace();
            }

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

    View.OnClickListener toggleColumns(final Button button)  {
        return new View.OnClickListener() {
            public void onClick(View v) {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                table_layout.setColumnCollapsed(7, !table_layout.isColumnCollapsed(7));
                table_layout.setColumnCollapsed(8, !table_layout.isColumnCollapsed(8));
                table_layout.setColumnCollapsed(9, !table_layout.isColumnCollapsed(9));
                table_layout.setColumnCollapsed(10, !table_layout.isColumnCollapsed(10));
                table_layout.setColumnCollapsed(11, !table_layout.isColumnCollapsed(11));
                table_layout.setColumnCollapsed(12, !table_layout.isColumnCollapsed(12));
                if(table_layout.isColumnCollapsed(7))
                {
                    button.setText("show");
                }
                else
                {
                    button.setText("Hide");
                }
                    }
                });
            }
        };
    }

    View.OnClickListener noteclicker(final Button button, final String ordername)  {
        return new View.OnClickListener() {
            public void onClick(View v) {
                Intent myIntent = new Intent(OrderConfirmation.this,NotesView.class);
                myIntent.putExtra("SelectedON", ordername);
                startActivity(myIntent);
            }
        };
    }



    View.OnClickListener updateStation(final Button button, final int category, final int element,final int number)  {
        return new View.OnClickListener() {
            public void onClick(View v) {
                if(order.Categories.get(category).Elements.get(element).StationStatus[number]==true) {
                    order.Categories.get(category).Elements.get(element).StationStatus[number] =false;
                }else{
                    order.Categories.get(category).Elements.get(element).StationStatus[number] =true;
                }
                //send or to webserver
            }
        };
    }

    private void BuildTable(int rows,Order data) {
        ClearTable();
        for(int k = 0;k<data.Categories.size();k++){
            TableRow CategoryRow = new TableRow(this);
            CategoryRow.setLayoutParams(new TableLayout.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));
            TextView CategoryName = new TextView(this);
            CategoryName.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                    TableRow.LayoutParams.WRAP_CONTENT));
            CategoryName.setBackgroundResource(R.drawable.cell_shape);
            CategoryName.setPadding(40, 40, 40, 40);
            CategoryName.setId(CategoryName.generateViewId());
            CategoryName.setText(data.Categories.get(k).Name);
            addRow(CategoryRow);

        rows = data.Categories.get(k).Elements.size();
        int cols = 13;
        // outer for loop
        for (int i = 0; i <= rows-1; i++) {

            TableRow row = new TableRow(this);
            row.setLayoutParams(new TableLayout.LayoutParams(TableRow.LayoutParams.MATCH_PARENT,
                    TableRow.LayoutParams.WRAP_CONTENT));

            // inner for loop
            for (int j = 1; j <= cols; j++) {

                switch (j){
                    case 1:
                        //pos
                        TextView pos = new TextView(this);
                        pos.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        pos.setBackgroundResource(R.drawable.cell_shape);
                        pos.setPadding(40, 40, 40, 40);
                        pos.setId(pos.generateViewId());
                        pos.setText(data.Categories.get(k).Elements.get(i).Position);//hardcode knapper og links og labels
                        row.addView(pos);

                        break;
                    case 2:
                        //Element info
                        TextView info = new TextView(this);
                        info.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        info.setBackgroundResource(R.drawable.cell_shape);
                        info.setPadding(40, 40, 40, 40);
                        info.setId(info.generateViewId());
                        String elementinfo ="";
                        for (int n = 0;n<data.Categories.get(k).Elements.get(i).ElementInfo.size()-n;n++){
                            elementinfo += data.Categories.get(k).Elements.get(i).ElementInfo.get(n)+"\n";
                        }
                        info.setText(elementinfo);
                        row.addView(info);
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
                        amount.setText(data.Categories.get(k).Elements.get(i).Amount);//hardcode knapper og links og labels
                        row.addView(amount);
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
                        final Button showSpecieal = new Button(this);
                        showSpecieal.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        showSpecieal.setBackgroundResource(R.drawable.cell_shape);
                        showSpecieal.setPadding(40, 40, 40, 40);
                        showSpecieal.setText(data.OrderName);//hardcode knapper og links og labels
                        showSpecieal.setId(showSpecieal.generateViewId());
                        showSpecieal.setOnClickListener(toggleColumns(showSpecieal));
                        row.addView(showSpecieal);
                        break;
                    case 8:
                        //knap
                        final Button st4 = new Button(this);
                        st4.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        st4.setBackgroundResource(R.drawable.cell_shape);
                        st4.setPadding(40, 40, 40, 40);
                        st4.setText(data.OrderName);//hardcode knapper og links og labels
                        st4.setId(st4.generateViewId());
                        st4.setOnClickListener(updateStation(st4,k,i,j));
                        row.addView(st4);
                        break;
                    case 9:
                        //knap
                        final Button st5 = new Button(this);
                        st5.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        st5.setBackgroundResource(R.drawable.cell_shape);
                        st5.setPadding(40, 40, 40, 40);
                        st5.setText(data.OrderName);//hardcode knapper og links og labels
                        st5.setId(st5.generateViewId());
                        st5.setOnClickListener(updateStation(st5,k,i,j));
                        row.addView(st5);
                        break;
                    case 10:
                        //knap
                        final Button st6 = new Button(this);
                        st6.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        st6.setBackgroundResource(R.drawable.cell_shape);
                        st6.setPadding(40, 40, 40, 40);
                        st6.setText(data.OrderName);//hardcode knapper og links og labels
                        st6.setId(st6.generateViewId());
                        st6.setOnClickListener(updateStation(st6,k,i,j));
                        row.addView(st6);
                        break;
                    case 11:
                        //knap
                        final Button st7 = new Button(this);
                        st7.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        st7.setBackgroundResource(R.drawable.cell_shape);
                        st7.setPadding(40, 40, 40, 40);
                        st7.setText(data.OrderName);//hardcode knapper og links og labels
                        st7.setId(st7.generateViewId());
                        st7.setOnClickListener(updateStation(st7,k,i,j));
                        row.addView(st7);
                        break;
                    case 12:
                        //knap
                        final Button st8 = new Button(this);
                        st8.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        st8.setBackgroundResource(R.drawable.cell_shape);
                        st8.setPadding(40, 40, 40, 40);
                        st8.setText(data.OrderName);//hardcode knapper og links og labels
                        st8.setId(st8.generateViewId());
                        st8.setOnClickListener(updateStation(st8,k,i,j));
                        row.addView(st8);
                    case 13:
                        //button
                        final String ordername = data.OrderName;
                        Button btn = new Button(this);
                        btn.setLayoutParams(new TableRow.LayoutParams(TableRow.LayoutParams.WRAP_CONTENT,
                                TableRow.LayoutParams.WRAP_CONTENT));
                        btn.setBackgroundResource(R.drawable.cell_shape);
                        btn.setPadding(5, 5, 5, 5);
                        btn.setId(btn.generateViewId());
                        btn.setOnClickListener(noteclicker(btn,ordername));

                        btn.setText("Notes");//hardcode knapper og links og labels
                        row.addView(btn);
                        break;

                }




            }


            addRow(row);
        }
        }
    }
}
