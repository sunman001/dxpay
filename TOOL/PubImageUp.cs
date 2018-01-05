using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace JMP.TOOL
{
    public class PubImageUp
    {
        /// <summary>
        /// 上传图片(上传到物理路径的方法)
        /// </summary>
        /// <param name="fileName">上传控件名称（name）</param>
        /// <param name="Wjurl">上传文件保存路径</param>
        /// <returns>返回一个数组（0：图片名称，1：图片路径，返回代码中（0如果等于99那么说明图片格式不正确））</returns>
        public static string[] UpImages(string fileName, string savepath)
        {
            string[] result = { null, null };
            HttpPostedFile uploadFile = System.Web.HttpContext.Current.Request.Files[fileName];
            if (uploadFile != null)
            {
                string upImg = uploadFile.FileName;
                string ext = Path.GetExtension(upImg);
                string expicName = "jpg,jpeg,png,bmp";//允许上传的图片的扩展名
                FileInfo fname = new FileInfo(upImg);
                if (expicName.IndexOf(fname.Extension.Substring(1).ToLower()) > -1)
                {
                    int FileLength = uploadFile.ContentLength;
                    Byte[] FileByteArray = new Byte[FileLength];
                    Stream StreamObject = uploadFile.InputStream;
                    StreamObject.Read(FileByteArray, 0, FileLength);
                    //result = MakeThumbnail(StreamObject, savepath, ".jpg");
                    string TFileName = "", ImageName = "";
                    try
                    {
                        Random rad = new Random();//实例化随机数产生器rad；
                        int value = rad.Next(1000, 10000);//用rad生成大于等于1000，小于等于9999的随机数；
                        string oStringTime = DateTime.Now.ToString("yyMMddHHmmssffffff");
                        if (TFileName == "")
                        {
                            TFileName = oStringTime + value.ToString() + ".jpg";
                            ImageName = TFileName;
                        }
                        //string SavePath = HttpContext.Current.Server.MapPath("~") + "\\" + savepath + "\\";
                        string SavePath = savepath;
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);//在根目录下建立文件夹
                        }
                        TFileName = SavePath + TFileName;  //开始保存图片至服务器
                        switch (ext)//保存图片格式（jpg,gif,bmp,png）
                        {
                            case ".jpeg":
                            case ".jpg":
                                uploadFile.SaveAs(TFileName);
                                break;
                            case ".gif":
                                uploadFile.SaveAs(TFileName);
                                break;
                            case ".bmp":
                                uploadFile.SaveAs(TFileName);
                                break;
                            case ".png":
                                uploadFile.SaveAs(TFileName);
                                break;
                        }
                        result[0] = ImageName;
                        result[1] = savepath.Substring(2) + ImageName;
                    }
                    catch (System.Exception e)
                    {
                        result[0] = "99";
                        result[1] = e.Message;
                    }
                }
                else
                {
                    result[0] = "99";
                    result[1] = "上传格式错误！";
                }
            }
            return result;
        }


        /// <summary>
        /// 上传图片（上传到虚拟路径的方法）
        /// </summary>
        /// <param name="fileName">上传控件名称（name）</param>
        /// <param name="Wjurl">上传文件保存路径</param>
        /// <returns>返回一个数组（0：图片名称，1：图片路径，返回代码中（0如果等于99那么说明图片格式不正确））</returns>
        public static string[] UpnewImages(string fileName, string savepath)
        {
            string[] result = { null, null };
            HttpPostedFile uploadFile = System.Web.HttpContext.Current.Request.Files[fileName];
            if (uploadFile != null)
            {
                string upImg = uploadFile.FileName;
                string ext = Path.GetExtension(upImg);
                string expicName = "jpg,jpeg,png,bmp";//允许上传的图片的扩展名
                FileInfo fname = new FileInfo(upImg);
                if (expicName.IndexOf(fname.Extension.Substring(1).ToLower()) > -1)
                {
                    int FileLength = uploadFile.ContentLength;
                    Byte[] FileByteArray = new Byte[FileLength];
                    Stream StreamObject = uploadFile.InputStream;
                    StreamObject.Read(FileByteArray, 0, FileLength);
                    //result = MakeThumbnail(StreamObject, savepath, ".jpg");
                    string TFileName = "", ImageName = "";
                    try
                    {
                        Random rad = new Random();//实例化随机数产生器rad；
                        int value = rad.Next(1000, 10000);//用rad生成大于等于1000，小于等于9999的随机数；
                        string oStringTime = DateTime.Now.ToString("yyMMddHHmmssffffff");
                        if (TFileName == "")
                        {
                            TFileName = oStringTime + value.ToString() + ".jpg";
                            ImageName = TFileName;
                        }
                        string SavePath = HttpContext.Current.Server.MapPath("~") + "\\" + savepath + "\\";
                        if (!Directory.Exists(SavePath))
                        {
                            Directory.CreateDirectory(SavePath);//在根目录下建立文件夹
                        }
                        TFileName = SavePath + TFileName;  //开始保存图片至服务器
                        switch (ext)//保存图片格式（jpg,gif,bmp,png）
                        {
                            case ".jpeg":
                            case ".jpg":
                                uploadFile.SaveAs(TFileName);
                                break;
                            case ".gif":
                                uploadFile.SaveAs(TFileName);
                                break;
                            case ".bmp":
                                uploadFile.SaveAs(TFileName);
                                break;
                            case ".png":
                                uploadFile.SaveAs(TFileName);
                                break;
                        }
                        result[0] = ImageName;
                        result[1] = savepath + ImageName;
                    }
                    catch (System.Exception e)
                    {
                        result[0] = "99";
                        result[1] = e.Message;
                    }
                }
                else
                {
                    result[0] = "99";
                    result[1] = "上传格式错误！";
                }
            }
            return result;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileName">上传控件名称（name）</param>
        /// <param name="Wjurl">上传文件保存路径</param>
        /// <returns>返回一个数组（0：图片名称，1：图片路径，返回代码中（0如果等于99那么说明图片格式不正确））</returns>
        public static string[] UpImagesS(string fileName, string savepath)
        {
            string[] result = { null, null };
            HttpPostedFile uploadFile = System.Web.HttpContext.Current.Request.Files[fileName];
            if (uploadFile != null)
            {
                string upImg = uploadFile.FileName;
                string expicName = "jpg,jpeg,png,bmp";//允许上传的图片的扩展名
                FileInfo fname = new FileInfo(upImg);
                if (expicName.IndexOf(fname.Extension.Substring(1).ToLower()) > -1)
                {
                    int FileLength = uploadFile.ContentLength;
                    Byte[] FileByteArray = new Byte[FileLength];
                    Stream StreamObject = uploadFile.InputStream;
                    StreamObject.Read(FileByteArray, 0, FileLength);
                    result = MakeThumbnail(StreamObject, savepath, ".jpg");
                }
                else
                {
                    result[0] = "99";
                    result[1] = "上传格式错误！";
                }
            }
            return result;
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="StreamObject">Stream数据</param>
        /// <param name="Path">保存路径（如：/images/pjgl/）</param>
        /// <param name="ext">后缀名（如：.jpg）</param>
        /// <returns>返回一个数组（0：图片名称，1：图片路径）</returns>
        public static string[] MakeThumbnail(Stream StreamObject, string Path, string ext)
        {
            Image originalImage = Image.FromStream(StreamObject);
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;//图片本身宽度
            int oh = originalImage.Height;//图片本身高度
            //获取图片的最大宽度和高度
            //int LimitWidth = Int32.Parse(ConfigurationSettings.AppSettings["ImgMaxWidth"]);
            //int LimitHeight = Int32.Parse(ConfigurationSettings.AppSettings["ImgMaxHeight"]);
            string TFileName = "", ImageName = "";
            string[] result = { null, null };
            //int width = ow > LimitWidth ? LimitWidth : ow;
            //int height = oh > LimitHeight ? LimitHeight : oh;
            Image bitmap = new System.Drawing.Bitmap(ow, oh); //新建一个originalImage图片
            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.White);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, ow, oh), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
            try
            {
                Random rad = new Random();//实例化随机数产生器rad；
                int value = rad.Next(1000, 10000);//用rad生成大于等于1000，小于等于9999的随机数；
                string oStringTime = DateTime.Now.ToString("yyMMddHHmmssffffff");
                if (TFileName == "")
                {
                    TFileName = oStringTime + value.ToString() + ext;
                    ImageName = TFileName;
                }
                //string SavePath = HttpContext.Current.Server.MapPath("~") + "\\" + Path + "\\";
                string SavePath = Path;
                if (!Directory.Exists(SavePath))
                    Directory.CreateDirectory(SavePath);//在根目录下建立文件夹
                TFileName = SavePath + TFileName;  //开始保存图片至服务器
                switch (ext)//保存图片格式（jpg,gif,bmp,png）
                {
                    case ".jpeg":
                    case ".jpg":
                        bitmap.Save(TFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".gif":
                        bitmap.Save(TFileName, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case ".bmp":
                        bitmap.Save(TFileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case ".png":
                        bitmap.Save(TFileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
                result[0] = ImageName;
                result[1] = Path.Substring(2) + ImageName;
            }
            catch (System.Exception e)
            {
                result[0] = "99";
                result[1] = e.Message;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public static void DeleteImage(string url)
        {
            //string path = System.Web.HttpContext.Current.Server.MapPath("~" + url);
            string path = url;
            if (File.Exists(path))
                File.Delete(path);//删除文件
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public static string[] Upload(string fileName, string savepath)
        {
            string[] result = { null, null };
            HttpPostedFile uploadFile = System.Web.HttpContext.Current.Request.Files[fileName];
            string strPath = System.Web.HttpContext.Current.Server.MapPath("~/upload/");//上传地址
            if (uploadFile != null)
            {
                string upImg = uploadFile.FileName;
                string expicName = "jpg,jpeg,png,bmp";//允许上传的图片的扩展名
                FileInfo fname = new FileInfo(upImg);
                if (expicName.IndexOf(fname.Extension.Substring(1).ToLower()) > -1)
                {
                    int FileLength = uploadFile.ContentLength;
                    Byte[] FileByteArray = new Byte[FileLength];
                    Stream StreamObject = uploadFile.InputStream;
                    StreamObject.Read(FileByteArray, 0, FileLength);
                    result = MakeThumbnail(StreamObject, savepath, ".jpg");

                    if (!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);//在根目录下建立文件夹
                    }
                    //string strName = uploadFile.Current.Request.Files[0].FileName;
                    //context.Request.Files[0].SaveAs(System.IO.Path.Combine(strPath, strName));
                }
                else
                {
                    result[0] = "99";
                    result[1] = "上传格式错误！";
                }
            }
            return result;
        }

    }
}
