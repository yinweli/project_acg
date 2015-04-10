/**
 * @file random.cs
 * @note random組件
 * @author yinweli
 */
//-----------------------------------------------------------------------------
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_IPHONE || UNITY_ANDROID
#define UNITY
#endif
//-----------------------------------------------------------------------------
#if UNITY
using UnityEngine;
#endif
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
//-----------------------------------------------------------------------------
namespace LibCSNStandard
{
	public class RandObject
	{
#if UNITY
		//-------------------------------------
		/**
		 * @brief 設置隨機種子
		 * @param iSeed 種子值
		 */
		public static void Seed(int iSeed)
		{
			UnityEngine.Random.seed = iSeed;
		}
		/**
		 * @brief 取得隨機值
		 * @param iMin 最小值
		 * @param iMax 最大值
		 * @return 隨機值
		 */
		public static int Next(int iMin, int iMax)
		{
			return UnityEngine.Random.Range(iMin, iMax);
		}
		/**
		 * @brief 取得隨機值
		 * @param iMax 最大值
		 * @return 隨機值
		 */
		public static int Next(int iMax)
		{
			return UnityEngine.Random.Range(0, iMax);
		}
		/**
		 * @brief 取得隨機值
		 * @param fMin 最小值
		 * @param fMax 最大值
		 * @return 隨機值
		 */
		public static float Next(float fMin, float fMax)
		{
			return UnityEngine.Random.Range(fMin, fMax);
		}
		/**
		 * @brief 取得隨機值
		 * @param fMax 最大值
		 * @return 隨機值
		 */
		public static float Next(float fMax)
		{
			return UnityEngine.Random.Range(0.0f, fMax);
		}
		//-------------------------------------
#else
		//-------------------------------------
		private static System.Random m_Rand = new System.Random();
		//-------------------------------------
		/**
		 * @brief 設置隨機種子
		 * @param iSeed 種子值
		 */
		public static void Seed(int iSeed)
		{
			m_Rand = new System.Random(iSeed);
		}
		/**
		 * @brief 取得隨機值
		 * @param iMin 最小值
		 * @param iMax 最大值
		 * @return 隨機值
		 */
		public static int Next(int iMin, int iMax)
		{
			return m_Rand.Next(iMin, iMax);
		}
		/**
		 * @brief 取得隨機值
		 * @param iMax 最大值
		 * @return 隨機值
		 */
		public static int Next(int iMax)
		{
			return m_Rand.Next(iMax);
		}
		//-------------------------------------
#endif
	}
}
//-----------------------------------------------------------------------------