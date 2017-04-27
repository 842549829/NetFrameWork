using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Utility
{
    /// <summary>
    /// Jquery
    /// </summary>
    public static class Jquery
    {
        /// <summary>
        /// 根据class+label获取所以子元素
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="label">标签</param>
        /// <param name="className">class</param>
        /// <returns>结果</returns>
        public static string GetElementByClassName(this string htmlConetnt, string label, string className)
        {
            return GetElement(htmlConetnt, label, "class", className);
        }

        /// <summary>
        /// 根据id+label获取所有子元素
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="label">标签</param>
        /// <param name="id">id</param>
        /// <returns>结果</returns>
        public static string GetElementById(this string htmlConetnt, string label, string id)
        {
            return GetElement(htmlConetnt, label, "id", id);
        }

        /// <summary>
        /// 根据id获取input的元素
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="id">id</param>
        /// <returns>结果</returns>
        public static string GetInputById(this string htmlConetnt, string id)
        {
            Regex reg = new Regex(string.Format(@"<input\b[^<>]*?\bid[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*{0}[^<>]*?/?[\s\t\r\n]*>", id), RegexOptions.IgnoreCase);
            Match first = reg.Match(htmlConetnt);
            return first.Value;
        }

        /// <summary>
        /// 根据id获取input的元素
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="name">name</param>
        /// <returns>结果</returns>
        public static string GetInputByName(this string htmlConetnt, string name)
        {
            Regex reg = new Regex(string.Format(@"<input\b[^<>]*?\bname[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*{0}[^<>]*?/?[\s\t\r\n]*>", name), RegexOptions.IgnoreCase);
            Match first = reg.Match(htmlConetnt);
            return first.Value;
        }

        /// <summary>
        /// 根据id获取input的元素
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="className">id</param>
        /// <returns>结果</returns>
        public static string GetInputByClassName(this string htmlConetnt, string className)
        {
            Regex reg = new Regex(string.Format(@"<input\b[^<>]*?\bclass[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*{0}[^<>]*?/?[\s\t\r\n]*>", className), RegexOptions.IgnoreCase);
            Match first = reg.Match(htmlConetnt);
            return first.Value;
        }

        /// <summary>
        /// 根据id获取input的元素
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="className">id</param>
        /// <returns>结果</returns>
        public static List<string> GetInputByClassNames(this string htmlConetnt, string className)
        {
            Regex reg = new Regex(string.Format(@"<input\b[^<>]*?\bclass[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*{0}[^<>]*?/?[\s\t\r\n]*>", className), RegexOptions.IgnoreCase);
            MatchCollection matchCollection = reg.Matches(htmlConetnt);
            return (from Match item in matchCollection select item.Value).ToList();
        }

        /// <summary>
        /// 根据id获取input的value值
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="id">id</param>
        /// <returns>结果</returns>
        public static string GetInputValueById(this string htmlConetnt, string id)
        {
            htmlConetnt = GetInputById(htmlConetnt,id);
            Regex reg = new Regex(@"<input\b[^<>]*?\bvalue[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<val>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            Match first = reg.Match(htmlConetnt);
            return first.Groups["val"].ToString();
        }

        /// <summary>
        /// 根据id获取input的value值
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="name">name</param>
        /// <returns>结果</returns>
        public static string GetInputValueByName(this string htmlConetnt, string name)
        {
            htmlConetnt = GetInputByName(htmlConetnt, name);
            Regex reg = new Regex(@"<input\b[^<>]*?\bvalue[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<val>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            Match first = reg.Match(htmlConetnt);
            return first.Groups["val"].ToString();
        }

        /// <summary>
        /// 根据id获取input的value值
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="className">className</param>
        /// <returns>结果</returns>
        public static string GetInputValueByClassName(this string htmlConetnt, string className)
        {
            htmlConetnt = GetInputByClassName(htmlConetnt, className);
            Regex reg = new Regex(@"<input\b[^<>]*?\bvalue[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<val>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            Match first = reg.Match(htmlConetnt);
            return first.Groups["val"].ToString();
        }

        /// <summary>
        /// 根据label+attribute+value获取所以的子元素
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="label">标签</param>
        /// <param name="attribute">属性</param>
        /// <param name="value">值</param>
        /// <param name="regexOptions">regexOptions</param>
        /// <returns>结果</returns>
        public static string GetElement(this string htmlConetnt, string label, string attribute, string value, RegexOptions regexOptions = RegexOptions.IgnoreCase)
        {
            Regex reg = new Regex(string.Format(@"(?is)<{0}\b[^<>]*?\b{2}[\s\t\r\n]*=[\s\t\r\n]*[""']{1}[""'][^>]*>(?><{0}[^>]*>(?<o>)|</{0}>(?<-o>)|(?:(?!</?{0}\b).)*)*(?(o)(?!))<[\s\t\r\n]*/[\s\t\r\n]*{0}[\s\t\r\n]*>", label, value, attribute), regexOptions);
            Match first = reg.Match(htmlConetnt);
            return first.Value;
        }

        /// <summary>
        /// 根据label获取所以子元素
        /// </summary>
        /// <param name="htmlConetnt">内容</param>
        /// <param name="label">标签</param>
        /// <param name="regexOptions">regexOptions</param>
        /// <returns>结果</returns>
        public static string GetElement(this string htmlConetnt, string label, RegexOptions regexOptions = RegexOptions.IgnoreCase)
        {
            Regex reg = new Regex(string.Format(@"(?is)<{0}[^>]*>(?><{0}[^>]*>(?<o>)|</{0}>(?<-o>)|(?:(?!</?{0}\b).)*)*(?(o)(?!))</{0}>", label), regexOptions);
            Match first = reg.Match(htmlConetnt);
            return first.Value;
        }

        /// <summary>
        /// 根据class+label获取元素集
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="label">标签</param>
        /// <param name="className">class</param>
        /// <returns>结果</returns>
        public static List<string> GetElementByClassNames(this string htmlConetnt, string label, string className)
        {
            return GetElements(htmlConetnt, label, "class", className);
        }

        /// <summary>
        /// 根据label+id获取元素集
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="label">标签</param>
        /// <param name="id">id</param>
        /// <returns>结果</returns>
        public static List<string> GetElementByIds(this string htmlConetnt, string label, string id)
        {
            return GetElements(htmlConetnt, label, "id", id);
        }

        /// <summary>
        /// 根据label+attribute+value获取元素集
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="label">标签</param>
        /// <param name="attribute">属性</param>
        /// <param name="value">值</param>
        /// <param name="regexOptions">regexOptions</param>
        /// <returns>结果</returns>
        public static List<string> GetElements(this string htmlConetnt, string label, string attribute, string value, RegexOptions regexOptions = RegexOptions.IgnoreCase)
        {
            Regex reg = new Regex(string.Format(@"(?is)<{0}\b[^<>]*?\b{2}[\s\t\r\n]*=[\s\t\r\n]*[""']{1}[""'][^>]*>(?><{0}[^>]*>(?<o>)|</{0}>(?<-o>)|(?:(?!</?{0}\b).)*)*(?(o)(?!))<[\s\t\r\n]*/[\s\t\r\n]*{0}[\s\t\r\n]*>", label, value, attribute), regexOptions);
            MatchCollection matchCollection = reg.Matches(htmlConetnt);
            return (from Match item in matchCollection select item.Value).ToList();
        }

        /// <summary>
        /// 根据label获取元素集
        /// </summary>
        /// <param name="htmlConetnt">内容</param>
        /// <param name="label">标签</param>
        /// <param name="regexOptions">regexOptions</param>
        /// <returns>结果</returns>
        public static List<string> GetElements(this string htmlConetnt, string label, RegexOptions regexOptions = RegexOptions.IgnoreCase)
        {
            Regex reg = new Regex(string.Format(@"(?is)<{0}[^>]*>(?><{0}[^>]*>(?<o>)|</{0}>(?<-o>)|(?:(?!</?{0}\b).)*)*(?(o)(?!))</{0}>", label), regexOptions);
            MatchCollection matchCollection = reg.Matches(htmlConetnt);
            return (from Match item in matchCollection select item.Value).ToList();
        }

        /// <summary>
        /// 根据label获取attribute的值
        /// </summary>
        /// <param name="htmlConetnt">html内容</param>
        /// <param name="label">标签</param>
        /// <param name="attribute">属性</param>
        /// <param name="regexOptions">regexOptions</param>
        /// <returns>值</returns>
        public static string Attribute(this string htmlConetnt, string label, string attribute, RegexOptions regexOptions = RegexOptions.IgnoreCase)
        {
            Regex reg = new Regex(string.Format(@"<{0}\b[^<>]*?\b{1}[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<val>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", label, attribute), regexOptions);
            Match first = reg.Match(htmlConetnt);
            return first.Groups["val"].ToString();
        }
    }
}