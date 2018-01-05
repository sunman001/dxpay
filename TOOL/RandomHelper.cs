using System;

namespace TOOL
{
    public class RandomHelper
    {
        private static readonly Random RanA = new Random();
        #region 生成指定长度的随机字符串
        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="intLength">随机字符串长度</param>
        /// <param name="booNumber">生成的字符串中是否包含数字</param>
        /// <param name="booSign">生成的字符串中是否包含符号</param>
        /// <param name="booSmallword">生成的字符串中是否包含小写字母</param>
        /// <param name="booBigword">生成的字符串中是否包含大写字母</param>
        /// <returns></returns>
        public static string GetRandomizer(int intLength, bool booNumber, bool booSign, bool booSmallword, bool booBigword)
        {
            //定义  

            var intResultRound = 0;
            var strB = "";

            while (intResultRound < intLength)
            {
                //生成随机数A，表示生成类型  
                //1=数字，2=符号，3=小写字母，4=大写字母  

                var intA = RanA.Next(1, 5);

                //如果随机数A=1，则运行生成数字  
                //生成随机数A，范围在0-10  
                //把随机数A，转成字符  
                //生成完，位数+1，字符串累加，结束本次循环  

                if (intA == 1 && booNumber)
                {
                    intA = RanA.Next(0, 10);
                    strB = intA + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }

                //如果随机数A=2，则运行生成符号  
                //生成随机数A，表示生成值域  
                //1：33-47值域，2：58-64值域，3：91-96值域，4：123-126值域  

                if (intA == 2 && booSign)
                {
                    intA = RanA.Next(1, 5);

                    //如果A=1  
                    //生成随机数A，33-47的Ascii码  
                    //把随机数A，转成字符  
                    //生成完，位数+1，字符串累加，结束本次循环  

                    if (intA == 1)
                    {
                        intA = RanA.Next(33, 48);
                        strB = ((char)intA) + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=2  
                    //生成随机数A，58-64的Ascii码  
                    //把随机数A，转成字符  
                    //生成完，位数+1，字符串累加，结束本次循环  

                    if (intA == 2)
                    {
                        intA = RanA.Next(58, 65);
                        strB = ((char)intA) + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=3  
                    //生成随机数A，91-96的Ascii码  
                    //把随机数A，转成字符  
                    //生成完，位数+1，字符串累加，结束本次循环  

                    if (intA == 3)
                    {
                        intA = RanA.Next(91, 97);
                        strB = ((char)intA) + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                    //如果A=4  
                    //生成随机数A，123-126的Ascii码  
                    //把随机数A，转成字符  
                    //生成完，位数+1，字符串累加，结束本次循环  

                    if (intA == 4)
                    {
                        intA = RanA.Next(123, 127);
                        strB = ((char)intA) + strB;
                        intResultRound = intResultRound + 1;
                        continue;
                    }

                }

                //如果随机数A=3，则运行生成小写字母  
                //生成随机数A，范围在97-122  
                //把随机数A，转成字符  
                //生成完，位数+1，字符串累加，结束本次循环  

                if (intA == 3 && booSmallword)
                {
                    intA = RanA.Next(97, 123);
                    strB = ((char)intA) + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }

                //如果随机数A=4，则运行生成大写字母  
                //生成随机数A，范围在65-90  
                //把随机数A，转成字符  
                //生成完，位数+1，字符串累加，结束本次循环  

                if (intA == 4 && booBigword)
                {
                    intA = RanA.Next(65, 89);
                    strB = ((char)intA) + strB;
                    intResultRound = intResultRound + 1;
                    continue;
                }
            }
            return strB;

        }
        #endregion
    }
}
