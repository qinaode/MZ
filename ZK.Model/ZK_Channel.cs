/**  智客知识管理平台。
* ZK_Channel.cs
*
* 功 能： N/A
* 类 名： ZK_Channel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/13 12:16:42   N/A    初版
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
	/// 1
	/// </summary>
	[Serializable]
	public partial class ZK_Channel
	{
		public ZK_Channel()
		{}
		#region Model
		private int _channelid;
		private string _channelname;
		private string _channeldesc;
		/// <summary>
		/// 
		/// </summary>
		public int channelID
		{
			set{ _channelid=value;}
			get{return _channelid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string channelName
		{
			set{ _channelname=value;}
			get{return _channelname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string channelDesc
		{
			set{ _channeldesc=value;}
			get{return _channeldesc;}
		}
		#endregion Model

	}
}

