using System;
using System.Collections.Generic;
using System.Text;

class Employee
{
    // khởi tạo lớp nhân viên
}

class Item
{
    private double price;
    private double discount;
    private string name;

    public Item(double price, double discount, string name)
    {
        this.price = price;
        this.discount = discount;
        this.name = name;
    }

    public double Price
    {
        get { return price; }
    }

    public double Discount
    {
        get { return discount; }
    }

    public string Name
    {
        get { return name; }
    }
}

class BillLine
{
    private Item item;
    private int quantity;

    public BillLine(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    public Item Item
    {
        get { return item; }
        set { item = value; }
    }
}

class GroceryBill
{
    private Employee clerk;
    private List<BillLine> billLines;

    public GroceryBill(Employee clerk)
    {
        this.clerk = clerk;
        this.billLines = new List<BillLine>();
    }

    public virtual void Add(BillLine billLine)
    {
        billLines.Add(billLine);
    }

    public double GetTotal()
    {
        double total = 0.0;
        foreach (BillLine billLine in billLines)
        {
            total += billLine.Quantity * billLine.Item.Price;
        }
        return total;
    }

    public void PrintReceipt()
    {
        Console.WriteLine("Biên Lai: ");
        foreach (BillLine billLine in billLines)
        {
            Item item = billLine.Item;
            int quantity = billLine.Quantity;
            double price = item.Price * quantity;
            Console.WriteLine($"{quantity}x {item.Name} - ${price}");
        }
        Console.WriteLine($"Tổng: ${GetTotal()}");
    }
}

class DiscountBill : GroceryBill
{
    private bool preferred;
    private int discountCount;
    private double discountAmount;

    public DiscountBill(Employee clerk, bool preferred) : base(clerk)
    {
        this.preferred = preferred;
        this.discountCount = 0;
        this.discountAmount = 0.0;
    }

    public int GetDiscountCount()
    {
        return discountCount;
    }

    public double GetDiscountAmount()
    {
        return discountAmount;
    }

    public double GetDiscountPercent()
    {
        double total = base.GetTotal();
        return (discountAmount / total) * 100;
    }

    public override void Add(BillLine billLine)
    {
        base.Add(billLine);
        if (preferred && billLine.Item.Discount > 0.0)
        {
            int quantity = billLine.Quantity;
            double discount = billLine.Item.Discount;
            discountCount += quantity;
            discountAmount += discount * quantity;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Employee clerk = new Employee();
        Item item1 = new Item(1.35, 0.25, "Mục 1");
        Item item2 = new Item(2.50, 0.0, "Mục 2");
        Item item3 = new Item(5.99, 0.10, "Mục 3");

        BillLine billLine1 = new BillLine(item1, 2);
        BillLine billLine2 = new BillLine(item2, 1);
        BillLine billLine3 = new BillLine(item3, 3);

        GroceryBill bill = new GroceryBill(clerk);
        bill.Add(billLine1);
        bill.Add(billLine2);
        bill.Add(billLine3);

        DiscountBill discountBill = new DiscountBill(clerk, true);
        discountBill.Add(billLine1);
        discountBill.Add(billLine2);
        discountBill.Add(billLine3);

        Console.WriteLine("Hóa Đơn Tạp Hóa: ");
        bill.PrintReceipt();

        Console.WriteLine();

        Console.WriteLine("Hóa Đơn Giảm Giá: ");
        discountBill.PrintReceipt();

        Console.WriteLine();

        Console.WriteLine("Thông Tin Giảm Giá: ");
        Console.WriteLine("Số Lượng Giảm Giá:  " + discountBill.GetDiscountCount());
        Console.WriteLine("Số Tiền Giảm Giá: $" + discountBill.GetDiscountAmount());
        Console.WriteLine("Giảm Giá Phần Trăm: " + discountBill.GetDiscountPercent() + "%");
    }
}