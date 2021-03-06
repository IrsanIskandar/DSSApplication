﻿using DssApplicationPoster.DatabaseManagement;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.Areas.Administrator.Models
{
    public class MediaContentModel
    {
        public static DataTable GetListImages()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_LIST_IMAGES;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                dataAdapter.Fill(data);
                conn.Close();

                return data;
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return data;
        }

        public static List<ImageList> SetDataImageList(DataTable dataTable)
        {
            List<ImageList> result = new List<ImageList>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ImageList image = new ImageList();
                        DateTime date = Convert.ToDateTime(null);

                        image.NumberImage = row["NumberImage"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["NumberImage"]);
                        image.ImageName = row["ImageName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["ImageName"]);
                        image.ImageSize = row["ImageSize"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["ImageSize"]);
                        image.ImagePath = row["ImagePath"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["ImagePath"]);
                        image.IsShow = row["IsShow"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["IsShow"]);
                        image.CreateBy = row["CreateBy"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["CreateBy"]);
                        image.CreateDate = row["CreateDate"].Equals(DBNull.Value) == true ? date : Convert.ToDateTime(row["CreateDate"]);

                        result.Add(image);
                    }

                    return result;
                }
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return result;
        }

        public static DataTable GetListVideo()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_LIST_VIDEOS;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                dataAdapter.Fill(data);
                conn.Close();

                return data;
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return data;
        }

        public static List<VideoList> SetDataListVideo(DataTable dataTable)
        {
            List<VideoList> result = new List<VideoList>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        VideoList image = new VideoList();
                        DateTime date = Convert.ToDateTime(null);

                        image.NumberVideo = row["NumberVideo"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["NumberVideo"]);
                        image.VideoName = row["VideoName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["VideoName"]);
                        image.VideoSize = row["VideoSize"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["VideoSize"]);
                        image.VideoPath = row["VideoPath"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["VideoPath"]);
                        image.IsShow = row["IsShow"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["IsShow"]);
                        image.CreateBy = row["CreateBy"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["CreateBy"]);
                        image.CreateDate = row["CreateDate"].Equals(DBNull.Value) == true ? date : Convert.ToDateTime(row["CreateDate"]);

                        result.Add(image);
                    }

                    return result;
                }
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return result;
        }

        public static DataTable GetListTextRunning()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_LIST_TEXT_RUN;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                dataAdapter.Fill(data);
                conn.Close();

                return data;
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return data;
        }

        public static List<TextRunning> SetDataListTextRunning(DataTable dataTable)
        {
            List<TextRunning> result = new List<TextRunning>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        TextRunning textRun = new TextRunning();
                        DateTime date = Convert.ToDateTime(null);

                        textRun.NumberText = row["NumberText"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["NumberText"]);
                        textRun.TextName = row["TextName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["TextName"]);
                        textRun.IsShow = row["IsShow"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["IsShow"]);
                        textRun.CreateBy = row["CreateBy"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["CreateBy"]);
                        textRun.CreateDate = row["CreateDate"].Equals(DBNull.Value) == true ? date : Convert.ToDateTime(row["CreateDate"]);

                        result.Add(textRun);
                    }

                    return result;
                }
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return result;
        }

        public static DataTable GetCountUsers()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_COUNT_USERS;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                dataAdapter.Fill(data);
                conn.Close();

                return data;
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return data;
        }

        public static GetCountUsers SetDataCountUsers(DataTable dataTable)
        {
            GetCountUsers userCount = new GetCountUsers();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        userCount.CountUsers = row["CountUsers"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["CountUsers"]);
                    }

                    return userCount;
                }
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return userCount;
        }

        public static DataTable GetCountMediaContentImage()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_COUNT_MEDIA_CONTENT_IMAGE;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                dataAdapter.Fill(data);
                conn.Close();

                return data;
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return data;
        }

        public static GetCountContentImage SetDataCountContentImage(DataTable dataTable)
        {
            GetCountContentImage imageContent = new GetCountContentImage();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        imageContent.CountImage = row["CountImage"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["CountImage"]);
                    }

                    return imageContent;
                }
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return imageContent;
        }

        public static DataTable GetCountMediaContentVideo()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_COUNT_MEDIA_CONTENT_VIDEO;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                dataAdapter.Fill(data);
                conn.Close();

                return data;
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return data;
        }

        public static GetCountContentVideo SetCountContentVideo(DataTable dataTable)
        {
            GetCountContentVideo videoContent = new GetCountContentVideo();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        videoContent.CountVideo = row["CountVideo"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["CountVideo"]);
                    }

                    return videoContent;
                }
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return videoContent;
        }

        public static DataTable GetCountMediaContentTextRunning()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_COUNT_MEDIA_CONTENT_TEXT_RUN;
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                dataAdapter.Fill(data);
                conn.Close();

                return data;
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return data;
        }

        public static GetCountContentTextRun SetCountContentTextRun(DataTable dataTable)
        {
            GetCountContentTextRun textRun = new GetCountContentTextRun();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        textRun.CountTextRunning = row["CountTextRunning"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["CountTextRunning"]);
                    }

                    return textRun;
                }
            }
            catch (MySqlException sqlEx)
            {
                sqlEx.Message.ToString();
            }
            catch (ArgumentException arEx)
            {
                throw new ArgumentException("Your Exception : " + arEx.Message.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Your Exception : " + ex.Message.ToString());
            }

            return textRun;
        }
    }
}
