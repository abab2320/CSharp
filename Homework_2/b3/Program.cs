using System;

class Account
{
    private string accountNumber;
    private string password;
    private double balance;

    //读取
    public string AccountNumber => accountNumber;
    public virtual double Balance => balance;

    //初始化
    public Account(string accountNumber, string password, double initBalance = 0)
    {
        this.accountNumber = accountNumber;
        this.password = password;
        this.balance = initBalance;
    }

    //登录
    public bool getPassword(string inputPassword)
    {
        return password == inputPassword;
    }

    //存钱
    public virtual void Deposit(double amount)
    {
        if(amount > 0)
        {
            this.balance += amount;
        }
    }

    //取钱
    public virtual bool Withdraw(double amount)
    {
        if (amount > 0 && amount <= this.balance)
        {
            balance = balance - amount;
            return true;
        }
        else return false;
    }
}

//信用账户类
class CreditAccount : Account
{
    private double creditLimit;
    public override double Balance => base.Balance + creditLimit;

    public CreditAccount(string  accountNumber, string password, double initBalance, double creditLimit)
        :base(accountNumber, password,initBalance)
    {
        this.creditLimit = creditLimit;
    }

    public override bool Withdraw(double amount)
    {
        if(amount > 0 && amount <= Balance)
        {
            return base.Withdraw(amount);
        }
        return false;
    }
}

//银行类
class Bank
{
    private Dictionary<string, Account> accounts = new Dictionary<string, Account>();
    
    public Account this[string accountNumber] => accounts.ContainsKey(accountNumber) ? accounts[accountNumber] : null;

    //开户 分类是哪种账户
    public void OpenAccount(string accountNumber,string password, double initBalance = 0, double creditLimit = 0)
    {
        if(!accounts.ContainsKey(accountNumber))
        {
            if (creditLimit > 0)
            {
                accounts[accountNumber] = new CreditAccount(accountNumber, password, initBalance, creditLimit);
            }
            else
            {
                accounts[accountNumber] = new Account(accountNumber, password, initBalance);
            }
        }
    }
}


//取大额现金事件类
class BigMoneyArgs : EventArgs
{
    public string AccountNumber { get; }
    public double Amount { get; }
    public BigMoneyArgs(string accountNumber, double amount)
    {
        AccountNumber = accountNumber;
        Amount = amount;
    }
}
//ATM类
class ATM
{
    private Bank bank;
    private const double BigMoneyThreshold = 10000;//出发事件的阈值
    public event EventHandler<BigMoneyArgs> BigMoneyFetched; 
    public ATM(Bank bank)
    {
        this.bank = bank;
    }

    //取款
    public void Withdraw(string accountNumber,string password, double amount)
    {
        Account account = bank[accountNumber];
        if (account != null && account.getPassword(password))
        {
            if(account.Withdraw(amount))
            {
                Console.WriteLine("取款成功");
                //出发大额取款事件
                if(amount > BigMoneyThreshold)
                {
                    BigMoneyFetched?.Invoke(this, new BigMoneyArgs(accountNumber, amount));
                }
            }
            else
            {
                Console.WriteLine("余额不足");
            }
        }
        else
        {
            Console.WriteLine("账号或密码问题");
        }
    }
}

    // 测试入口
    class Program
    {
        static void Main()
        {
            Bank bank = new Bank();
            ATM atm = new ATM(bank);

            // 订阅大额取款事件
            atm.BigMoneyFetched += (sender, e) =>
            {
                Console.WriteLine($"警告！账户 {e.AccountNumber} 取走大额现金 {e.Amount} 元！");
            };

            // 创建账户
            bank.OpenAccount("123456", "password", 20000);

            // 测试正常取款
            atm.Withdraw("123456", "password", 5000);

            // 测试大额取款（应触发事件）
            atm.Withdraw("123456", "password", 15000);
        }
    }

