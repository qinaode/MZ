/**  智客知识管理平台。
* miniyun_options.cs
*
* 功 能： N/A
* 类 名： miniyun_options
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/30 10:55:28   N/A    初版
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
	/// miniyun_options:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class miniyun_options
	{
		public miniyun_options()
		{}
		#region Model
		private int _option_id;
		private string _option_name;
		private string _option_value;
		private DateTime _created_at;
		private DateTime _updated_at;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int option_id
		{
			set{ _option_id=value;}
			get{return _option_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string option_name
		{
			set{ _option_name=value;}
			get{return _option_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string option_value
		{
			set{ _option_value=value;}
			get{return _option_value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime created_at
		{
			set{ _created_at=value;}
			get{return _created_at;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime updated_at
		{
			set{ _updated_at=value;}
			get{return _updated_at;}
		}
		#endregion Model

	}
}

