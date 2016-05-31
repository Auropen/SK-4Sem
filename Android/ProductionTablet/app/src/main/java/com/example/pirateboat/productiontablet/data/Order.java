package com.example.pirateboat.productiontablet.data;

import java.util.ArrayList;

/**
 * Created by Swodah on 31-05-2016.
 */
public class Order {
    String id;

    ArrayList<String> AltDeliveryInfo = new ArrayList<String>();
    ArrayList<Category> Categories = new ArrayList<Category>();
    ArrayList<String> CompanyInfo = new ArrayList<String>();
    ArrayList<String> CustomerInfo = new ArrayList<String>();
    ArrayList<String> kitchenInfo = new ArrayList<String>();
    ArrayList<OrderNote> notes = new ArrayList<OrderNote>();

    @Override
    public String toString() {
        return id;
    }
}
