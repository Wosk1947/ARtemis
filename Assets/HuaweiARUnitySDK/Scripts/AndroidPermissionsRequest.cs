﻿/*
 * 2019.09.19 - Huawei modifies and matches AREngine.
 * Copyright (C) 2019. Huawei Technologies Co., Ltd. All rights reserved.
 */
//-----------------------------------------------------------------------
// <copyright file="AndroidPermissionsManager.cs" company="Google">
//
// Copyright 2017 Google LLC. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace HuaweiARUnitySDK
{
    using System;
    using System.Collections.Generic;
    using HuaweiARInternal;
    using UnityEngine;

    /**
     * \if english
     * @brief Manages Android permissions for the Unity application.
     * \else
     * @brief 管理Unity应用中的Android权限请求。
     * \endif
     */
    public class AndroidPermissionsRequest : AndroidJavaProxy
    {
        private static AndroidPermissionsRequest m_instance;
        private static AndroidJavaObject m_permissionRequestInJava;
        private static AsyncTask<AndroidPermissionsRequestResult> m_currentRequest = null;
        private static Action<AndroidPermissionsRequestResult> m_onPermissionsRequestFinished;

        ///@cond EXCLUDE_FROM_DOXYGEN
        /// <summary>
        /// Instance of this class.
        /// </summary>
        public static AndroidPermissionsRequest Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new AndroidPermissionsRequest();
                }
                return m_instance;
            }
        }
        /// @endcond

        ///@cond EXCLUDE_FROM_DOXYGEN
        /// <summary>
        /// UnityActivity.
        /// </summary>
        public static AndroidJavaObject UnityActivity
        {
            get
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject m_unityMainActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                return m_unityMainActivity;
            }
        }
        /// @endcond

        private static AndroidJavaObject AndroidPermissionsService
        {
            get
            {
                if (m_permissionRequestInJava == null)
                {
                    m_permissionRequestInJava = new AndroidJavaObject("com.huawei.hiar.UnityAndroidPermissions");
                }
                return m_permissionRequestInJava;
            }
        }

        /**
         * \if english
         * @brief Checks whether an Android permission is granted to the application.
         * @param permissionName The full name of the permission.
         * @return \c true when application is granted with \c permissionName, otherwise \c false.
         * \else
         * @brief 检查应用是否有Android权限。
         * @param permissionName 权限名
         * @return 应用有\c permissionName权限时，返回\c true，否则返回\c false。
         * \endif
         */
        public static bool IsPermissionGranted(string permissionName)
        {
            return AndroidPermissionsService.Call<bool>("IsPermissionGranted", UnityActivity, permissionName);
        }

        /**
         * \if english
         * @brief Requests an Android permission from the user.
         * @param permissionNames The full name array of the requested permissions.
         * @return An \link AsyncTask \endlink when the user has accepted/rejected the requested permission  and yields a
         * \link AndroidPermissionsRequestResult \endlink that summarizes the result. If this method is called when
         * other permissions request is pending, \c null will be returned instead.
         * \else
         * @brief 从Android系统中申请权限。
         * @param permissionNames 请求的权限名数组。
         * @return 当用户同意或者拒绝请求的权限时，返回一个\link AsyncTask \endlink，并将结果写入
         * \link AndroidPermissionsRequestResult \endlink。如果上一次请求还没有完成，该函数将返回\c null。
         * \endif
         */
        public static AsyncTask<AndroidPermissionsRequestResult> RequestPermission(string[] permissionNames)
        {
            if (m_currentRequest != null)
            {
                ARDebug.LogError("Do not make simultaneous permission requests.");
                return null;
            }

            AndroidPermissionsService.Call("RequestPermissionAsync", UnityActivity, permissionNames, Instance);
            m_currentRequest = new AsyncTask<AndroidPermissionsRequestResult>(out m_onPermissionsRequestFinished);

            return m_currentRequest;
        }

        ///@cond EXCLUDE_FROM_DOXYGEN
        /// <summary>
        /// constructor.
        /// </summary>
        public AndroidPermissionsRequest() : base("com.huawei.hiar.UnityAndroidPermissions$IPermissionRequestResult") { }
        ///@endcond

        ///@cond EXCLUDE_FROM_DOXYGEN
        ///@brief Callback when permissions are granted or rejected.
        ///@param result A json string generated by AndroidPermissonManager.aar
        public virtual void OnRequestPermissionsResult(string result)
        {
            var permissionResults = _parseString(result);
            if (m_onPermissionsRequestFinished == null)
            {
                Debug.LogError("AndroidPermissionsRequest error");
                return;
            }
            var onRequestFinished = m_onPermissionsRequestFinished;
            m_currentRequest = null;
            m_onPermissionsRequestFinished = null;
            onRequestFinished(new AndroidPermissionsRequestResult(permissionResults));
        }
        ///@endcond

        ///@cond EXCLUDE_FROM_DOXYGEN
        ///@brief Parse the json string of the result.
        ///@return The \link AndroidPermissionsRequestResult \endlink from the \c result.
        private AndroidPermissionsRequestResult.PermissionResult[] _parseString(string result)
        {
            string newJson = result.Substring(1, result.Length - 2);
            char[] seprator = { ',' };
            string[] results = newJson.Split(seprator);
            var permissionResultList =
                new List<AndroidPermissionsRequestResult.PermissionResult>();
            char[] itemSep = { ':' };
            foreach (var value in results)
            {
                string[] item = value.Substring(1, value.Length - 2).Split(itemSep);
                var pr = new AndroidPermissionsRequestResult.PermissionResult();
                pr.permissionName = item[0];
                pr.granted = int.Parse(item[1]);
                permissionResultList.Add(pr);
            }
            return permissionResultList.ToArray();
        }
        ///@endcond
    }
}
