using System.Text;

public class Account
    {
        public string sotk { get; set; }
        public string chush { get; set; }
        public string OwnerName { get; set; }
        public decimal Balance { get; set; }
        public decimal InterestRate { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Account(string accountNumber, string ownerID, string ownerName, decimal balance, decimal interestRate)
        {
            sotk = accountNumber;
            chush = ownerID;
            OwnerName = ownerName;
            Balance = balance;
            InterestRate = interestRate;
            Transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount, DateTime date)
        {
            Balance += amount;
            Transactions.Add(new Transaction(sotk, "Tiền Gửi", amount, date));
        }

        public void Withdraw(decimal amount, DateTime date)
        {
            if (amount > Balance)
            {
                Console.WriteLine("Số Dư Thiếu.");
                return;
            }

            Balance -= amount;
            Transactions.Add(new Transaction(sotk, "Rút", amount, date));
        }

        public void CalculateInterest()
        {
            decimal interest = Balance * InterestRate / 100;
            Balance += interest;
            Transactions.Add(new Transaction(sotk, "Kiểm Tra Số Dư", interest, DateTime.Now));
        }

        public void GenerateTransactionReport()
        {
            Console.WriteLine("Số Tài Khoản: " + sotk);
            Console.WriteLine("Số Dư: " + Balance);

            Console.WriteLine("Giao Dịch:");
            foreach (var transaction in Transactions)
            {
                Console.WriteLine("Thời Gian: " + transaction.Date);
                Console.WriteLine("Kiểu: " + transaction.TransactionType);
                Console.WriteLine("Số Lượng: " + transaction.Amount);
                Console.WriteLine("---------------");
            }
        }
    }

    public class Transaction
    {
        public string AccountNumber { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Transaction(string accountNumber, string transactionType, decimal amount, DateTime date)
        {
            AccountNumber = accountNumber;
            TransactionType = transactionType;
            Amount = amount;
            Date = date;
        }
    }

    public class Bank
    {
        public List<Account> Accounts { get; set; }

        public Bank()
        {
            Accounts = new List<Account>();
        }

        public void CreateAccount(string accountNumber, string ownerID, string ownerName, decimal balance, decimal interestRate)
        {
            Account account = new Account(accountNumber, ownerID, ownerName, balance, interestRate);
            Accounts.Add(account);
        }

        public Account GetAccount(string accountNumber)
        {
            return Accounts.Find(account => account.sotk == accountNumber);
        }

        public void Deposit(string accountNumber, decimal amount, DateTime date)
        {
            Account account = GetAccount(accountNumber);
            if (account != null)
            {
                account.Deposit(amount, date);
            }
            else
            {
                Console.WriteLine("Tài khoản không được tìm thấy.");
            }
        }

        public void Withdraw(string accountNumber, decimal amount, DateTime date)
        {
            Account account = GetAccount(accountNumber);
            if (account != null)
            {
                account.Withdraw(amount, date);
            }
            else
            {
                Console.WriteLine("Tài khoản không được tìm thấy.");
            }
        }

        public void CalculateInterest()
        {
            foreach (var account in Accounts)
            {
                account.CalculateInterest();
            }
        }

        public void GenerateReport()
        {
            foreach (var account in Accounts)
            {
                account.GenerateTransactionReport();
                Console.WriteLine("---------------");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Bank bank = new Bank();

            // Mở tài khoản mới
            bank.CreateAccount("001", "901", "Alice", 100, 5);
            bank.CreateAccount("002", "902", "Bob", 50, 5);
            bank.CreateAccount("003", "901", "Alice", 200, 10);
            bank.CreateAccount("004", "903", "Eve", 200, 10);

            // Nhập tiền vào tài khoản
            bank.Deposit("001", 100, new DateTime(2005, 7, 15));
            bank.Deposit("001", 100, new DateTime(2005, 8, 15));
            // Rút tiền từ tài khoản
            bank.Withdraw("001", 50, new DateTime(2005, 8, 1));

            // Tính lãi suất cho tất cả các tài khoản
            bank.CalculateInterest();

            // In báo cáo giao dịch của tất cả các tài khoản
            bank.GenerateReport();

            Console.ReadLine();
        }
    }