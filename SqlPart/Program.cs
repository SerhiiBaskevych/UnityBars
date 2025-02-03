using SqlPart.Model;


namespace SqlPart
{    
    public class Program
    {
        public static void Main()
        {
            SqlQueryBuilder generator = new SqlQueryBuilder();
            string sqlCondition;

            List<AccountCondition> PositiveConditions = new List<AccountCondition>
            {
                new AccountCondition { Id = 1, NBS = "2630", Flag_NBS = 1, OB22 = "12", Flag_OB22 = 1 },
                new AccountCondition { Id = 2, NBS = "2620", Flag_NBS = 1, OB22 = "36", Flag_OB22 = 0 },
            };

            
            sqlCondition = generator.GetSqlCondition(PositiveConditions);
            
            Console.WriteLine("Positive conditions:");
            Console.WriteLine(sqlCondition);

            List<AccountCondition> NegativeConditions = new List<AccountCondition>
            {                
                new AccountCondition { Id = 3, NBS = "2640", Flag_NBS = 0, OB22 = "13", Flag_OB22 = 1 },
                new AccountCondition { Id = 4, NBS = "2650", Flag_NBS = 0, OB22 = "14", Flag_OB22 = 0 }
            };

            sqlCondition = generator.GetSqlCondition(NegativeConditions);
            Console.WriteLine("Negative conditions:");
            Console.WriteLine(sqlCondition);
        }
    }
}

