package com.example.pirateboat.productiontablet.data;

import java.util.ArrayList;

/**
 * Created by Swodah on 31-05-2016.
 */
public class Order {





    public ArrayList<String> AltDeliveryInfo = new ArrayList<String>();
    public String AlternativeNumber;
    public ArrayList<Category> Categories = new ArrayList<Category>();
    public ArrayList<String> CompanyInfo = new ArrayList<String>();
    public ArrayList<String> CustomerInfo = new ArrayList<String>();
    public String OrderDate;
    public String OrderName;
    public String OrderNumber;
    public String ProducedDate;
    public String Week;
    //public OrderStatus orderStatuses = new OrderStatus();
    public ArrayList<String> kitchenInfo = new ArrayList<String>();
    //public ArrayList<OrderNote> notes = new ArrayList<OrderNote>();

    @Override
    public String toString() {
        return AlternativeNumber;
    }
}
