using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
namespace jQueryString
{
    public static class StringFormat
    {
        //模板字符串前缀
        private static readonly string __prefix = "$";
        //￥ $ 正则表达式 $@person.name
        private static readonly Regex VariableRegex = new Regex(@"\$(@{0,1}[a-zA-Z_\.0-9]+)");
        
        //String扩展方法
        public static string jQueryStringFormat(this String @this, string sjQueryStringT, object oValue)
        {
       
            //检测验证
            if (string.IsNullOrEmpty(sjQueryStringT))
                return sjQueryStringT;
            if (!sjQueryStringT.Contains(__prefix))
                throw new Exception("字符串中变量不包含$前缀");
            if (oValue == null)
                return sjQueryStringT;

            //解析
            //need  using System.Linq;
            var variables = GetEnumerateVariables(sjQueryStringT).ToArray();
            foreach (string vname in variables)
            {
                //获取值
                string vvalue = ValueForName(oValue, vname).ToString();
                //字符串替换
                sjQueryStringT = sjQueryStringT.Replace("$" + vname, vvalue);
               

            }
            return sjQueryStringT;
        }
        /// <summary>
        /// 获取变量迭代,例如
        ///  var variables = GetEnumerateVariables("I love iphone $iphoneVersion,").ToArray();
        /// variables[0];//$iphoneVersion
        /// </summary>
        /// <param name="s">模板字符串</param>
        /// <returns></returns>
        private static IEnumerable<string> GetEnumerateVariables(string s)
        {
            var matchCollection = VariableRegex.Matches(s);

            for (int i = 0; i < matchCollection.Count; i++)
            {
                yield return matchCollection[i].Groups[1].Value;
            }
        }
        /// <summary>
        /// 获取变量数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Array GetVariables(string s)
        {
            return GetEnumerateVariables(s).ToArray();
        }
        /// <summary>
        /// 获取对象的对应属性值
        /// </summary>
        /// <param name="oValue">包含值的对象</param>
        /// <param name="name">属性名</param>
        /// <returns></returns>
        private static object ValueForName(object oValue, string name)
        {
            Type type = oValue.GetType();
            var property = type.GetProperty(name);
            if (property != null)
            {
                return property.GetValue(oValue, new object[0]);
            }

            var field = type.GetField(name);
            if (field != null)
            {
                return field.GetValue(oValue);
            }
            throw new FormatException("未找到命名参数: " + name);
        }
    }
}
