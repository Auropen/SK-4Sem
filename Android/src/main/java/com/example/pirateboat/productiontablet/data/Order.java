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
    public ArrayList<String> DeliveryInfo = new ArrayList<String>();
    public String HousingAssociation;
    public String OrderDate;
    public String OrderName;
    public String OrderNumber;
    public String ProducedDate;
    public String Week;
    public OrderStatus StationStatus = new OrderStatus();
    public ArrayList<String> KitchenInfo = new ArrayList<String>();
    public ArrayList<OrderNote> Notes = new ArrayList<OrderNote>();

    @Override
    public String toString() {
        return AlternativeNumber;
    }
}
