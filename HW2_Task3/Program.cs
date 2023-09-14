// HW2 Task3


string qStr = " Another receipt? (y/n)";
Console.WriteLine(qStr);

string userChoiceStr = Console.ReadLine();
userChoiceStr = userChoiceStr.ToLower();

string outMsgStr;

List<Receipt> receiptList = new List<Receipt>();    

// collect receipts or orders
while (userChoiceStr == "y" || userChoiceStr == "yes")
{
    outMsgStr = "Customer ID: ";
    Console.Write(outMsgStr);   
    string customerIDStr = Console.ReadLine();  
    int customerIDInt = Convert.ToInt32(customerIDStr);

    Console.Write("Number of COGS: ");
    string numCogsStr = Console.ReadLine();
    int numCogsInt = Convert.ToInt32(numCogsStr);

    Console.Write("Number of gears: ");
    string numGearsStr = Console.ReadLine();
    int numGearsInt = Convert.ToInt32(numGearsStr);


    Receipt newReceipt = new Receipt(customerIDInt, numGearsInt, numCogsInt);

    newReceipt.PrintReceipt();  

    receiptList.Add(newReceipt);

    Console.WriteLine();
    Console.Write(qStr);
    userChoiceStr = Console.ReadLine();
    userChoiceStr = userChoiceStr.ToLower();

}

// four options
//Please choose from the options:
//1: Print all receipt of one customer
//2: Print all receipt for today
//3: Print the highest total receipt
//Press other keys to end

userChoiceStr = "1";
while (userChoiceStr == "1" || userChoiceStr == "2" || userChoiceStr == "3")
{
    Console.WriteLine("Please choose from the options:");
    Console.WriteLine("1: Print all receipt of one customer");
    Console.WriteLine("2: Print all receipt for today");
    Console.WriteLine("3: Print the highest total receipt");
    Console.WriteLine("Press other keys to end");

    userChoiceStr = Console.ReadLine();
    userChoiceStr = userChoiceStr.ToLower();

    if (userChoiceStr == "1")
    {
        Console.WriteLine("Input Customer ID: ");
        string inputCustIDstr = Console.ReadLine();
        int inputCustIDint = Convert.ToInt32(inputCustIDstr);   

        for (int i = 0; i < receiptList.Count; i++)
        {
            if (receiptList[i].CustomerID == inputCustIDint)
            {
                receiptList[i].PrintReceipt();
            }
        }
    }
    else if (userChoiceStr == "2")
    {
        DateTime tdDT = DateTime.Now;

        for (int i = 0; i < receiptList.Count; i++)
        {
            if (receiptList[i].SaleDate.ToString("d") == tdDT.ToString("d"))
            {
                receiptList[i].PrintReceipt();
            }
            Console.WriteLine(receiptList[i].SaleDate.ToString("d"));
        }
    }
    else if(userChoiceStr == "3") 
    {
        if (receiptList.Count > 0)
        {
            Receipt highestR = receiptList[0];
            for (int i = 0; i < receiptList.Count; i++)
            {
                if (receiptList[i].CalculateTotal() > highestR.CalculateTotal())
                {
                    highestR = receiptList[i];
                }
            }
            Console.WriteLine("HIGHEST: ");
            highestR.PrintReceipt();
        }
    }
}

Console.ReadLine();

public class Receipt
{
    public int CustomerID;
    public int CogQuantity;
    public int GearQuantity;
    public DateTime SaleDate;
    private double SalesTaxPercent;
    private double CogPrice;
    private double GearPrice; 

    public Receipt() 
    {
        this.SalesTaxPercent = 8.9 / 100;
        this.CogPrice = 79.99;
        this.GearPrice = 250.00;

        this.SaleDate = DateTime.Now;
    } 
    
    public Receipt(int id, int cog, int gear)
    {
        this.SalesTaxPercent = 8.9 / 100;
        this.CogPrice = 79.99;
        this.GearPrice = 250.00;

        this.CustomerID = id;
        this.CogQuantity = cog;
        this.GearQuantity = gear;
        this.SaleDate = DateTime.Now;
    }

    public void PrintReceipt()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("RECEIPT");
        string outMesStr;
        outMesStr = string.Format($"# of cogs: {this.CogQuantity}");
        Console.WriteLine(outMesStr);

        outMesStr = string.Format($"# of gears: {this.GearQuantity}");
        Console.WriteLine(outMesStr);

        outMesStr = string.Format($"Net Amount: {this.CalculateNetAmount():C2}");
        Console.WriteLine(outMesStr);

        outMesStr = string.Format($"Tax Amount: {this.CalculateTaxAmount():C2}");
        Console.WriteLine(outMesStr);

        outMesStr = string.Format($"Total Amount: {this.CalculateTotal():C2}");
        Console.WriteLine(outMesStr);

        outMesStr = string.Format($"Tim: {this.SaleDate}");
        Console.WriteLine(outMesStr);
        Console.WriteLine("========================================");

    }

    private double CalculateNetAmount()
    {
     
        double netAmountDbl;
        if (this.CogQuantity > 10 || this.GearQuantity > 10 || this.CogQuantity+this.GearQuantity > 16)
        {
            netAmountDbl = (this.CogPrice * this.CogQuantity + this.GearPrice * this.GearQuantity) * (1 + 0.125);
        }
        else
        {
            netAmountDbl = (this.CogPrice * this.CogQuantity + this.GearPrice * this.GearQuantity) * (1 + 0.15);
        }
        return netAmountDbl;
    }

    private double CalculateTaxAmount()
    {
        return this.SalesTaxPercent * this.CalculateNetAmount();
    }

    public double CalculateTotal()
    {
        return this.CalculateNetAmount() + this.CalculateTaxAmount();
    }

}


