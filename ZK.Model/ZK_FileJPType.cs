/**  智客知识管理平台。
* ZK_FileJPType.cs
*
* 功 能： N/A
* 类 名： ZK_FileJPType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/24 22:37:49   N/A    初版
*
* Copyright (c) 2012 BeiJing HaoLian Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：北京浩联教育科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ZK.Model
{
	/// <summary>
	/// ZK_FileJPType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ZK_FileJPType
	{
		public ZK_FileJPType()
		{}
		#region Model
		private int _id;
		private string _typename;
		private string _typedesc;
		private bool _isopen;
		private string _imageurl;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeDesc
		{
			set{ _typedesc=value;}
			get{return _typedesc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool isOpen
		{
			set{ _isopen=value;}
			get{return _isopen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string imageURL
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		#endregion Model

	}
}

