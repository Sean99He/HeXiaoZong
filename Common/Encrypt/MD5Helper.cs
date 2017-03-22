﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Encrypt
{
	/// <summary>
	/// md5加密 created by Sean 2016-11-30 09:59:34
	/// </summary>
	public class MD5Helper
	{
		/// <summary>
		/// 密码加密 created by Sean 2016-11-30 11:42:43
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		public static string MD5Encrypt(string password)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] byteValue = Encoding.Default.GetBytes(password);
			//加密后数据
			byte[] result = md5.ComputeHash(byteValue);
			StringBuilder pw = new StringBuilder();
			//将字符型数组转换成字符串
			foreach (var item in result)
			{
				pw.Append(item.ToString("x"));//16制类型数据 ToString("x")结果为小写 ToString("X")结果为大写
			}
			return pw.ToString();
		}
		/// <summary>
		/// md5带偏移量的加密
		/// </summary>
		/// <param name="password"></param>
		/// <param name="move"></param>
		/// <returns></returns>
		public static string MD5EncryptWithMove(string password)
		{
			string move = "move";//默认key
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] byteValue = Encoding.ASCII.GetBytes(password + move);
			byte[] result = md5.ComputeHash(byteValue);
			StringBuilder pw = new StringBuilder();
			foreach (var item in result)
			{
				pw.Append(item.ToString("x").PadLeft(2, '0'));
			}
			return pw.ToString();
		}
	}
}
