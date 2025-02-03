using SqlPart.Model;
using System.Text;

namespace SqlPart
{
    public class SqlQueryBuilder
    {
        public string GetSqlCondition(List<AccountCondition> conditions)
        {
            StringBuilder sqlCondition = new StringBuilder();

            foreach (var condition in conditions)
            {
                if (sqlCondition.Length > 0)
                {
                    sqlCondition.Append(" and ");
                }

                if (condition.Flag_NBS == 1)
                {
                    sqlCondition.Append($"(NBS = '{condition.NBS}'");
                }
                else
                {
                    sqlCondition.Append($"(NBS != '{condition.NBS}'");
                }

                if (condition.Flag_OB22 == 1)
                {
                    sqlCondition.Append($" and OB22 = '{condition.OB22}')");
                }
                else
                {
                    sqlCondition.Append($" and OB22 != '{condition.OB22}')");
                }
            }
            return sqlCondition.ToString();
        }
    }
}
