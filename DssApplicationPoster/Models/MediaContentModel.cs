using DssApplicationPoster.DatabaseManagement;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DssApplicationPoster.Models
{
    public class MediaContentModel
    {
        public static DataTable GetMediaContentListImage()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_MEDIA_CONTENT_IMAGE;
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

        public static List<ImageContent> SetDataMediaContentListImage(DataTable dataTable)
        {
            List<ImageContent> result = new List<ImageContent>();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ImageContent media = new ImageContent();
                        DateTime date = Convert.ToDateTime(null);

                        // Image Media
                        media.NumberImage = row["NumberImage"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["NumberImage"]);
                        media.ImageName = row["ImageName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["ImageName"]);
                        media.ImageSize = row["ImageSize"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["ImageSize"]);
                        media.ImagePath = row["ImagePath"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["ImagePath"]);
                        media.IsShowImage = row["IsShowImage"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["IsShowImage"]);

                        result.Add(media);
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

        public static DataTable GetMediaContentImage()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_MEDIA_CONTENT_IMAGE;
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

        public static ImageContent SetDataMediaContentImage(DataTable dataTable)
        {
            ImageContent result = new ImageContent();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ImageContent media = new ImageContent();
                        DateTime date = Convert.ToDateTime(null);

                        // Image Media
                        media.NumberImage = row["NumberImage"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["NumberImage"]);
                        media.ImageName = row["ImageName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["ImageName"]);
                        media.ImageSize = row["ImageSize"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["ImageSize"]);
                        media.ImagePath = row["ImagePath"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["ImagePath"]);
                        media.IsShowImage = row["IsShowImage"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["IsShowImage"]);
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

        public static DataTable GetMediaContentVideo()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_MEDIA_CONTENT_VIDEO;
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

        public static VideoContent SetDataMediaContentVideo(DataTable dataTable)
        {
            VideoContent result = new VideoContent();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        result.NumberVideo = row["NumberVideo"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["NumberVideo"]);
                        result.VideoName = row["VideoName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["VideoName"]);
                        result.VideoSize = row["VideoSize"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["VideoSize"]);
                        result.VideoPath = row["VideoPath"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["VideoPath"]);
                        result.IsShowVideo = row["IsShowVideo"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["IsShowVideo"]);
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

        public static DataTable GetMediaContentTextRun()
        {
            DataTable data = new DataTable();
            try
            {
                MySqlConnection conn = new MySqlConnection(Constants.ConnsStrings);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = QueryProcedureHelper.SP_GET_MEDIA_CONTENT_TEXTRUN;
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

        public static TextRunning SetDataMediaContentTextRun(DataTable dataTable)
        {
            TextRunning result = new TextRunning();

            try
            {
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        result.NumberText = row["NumberText"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(row["NumberText"]);
                        result.TextName = row["TextName"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["TextName"]);
                        result.IsShowText = row["IsShowText"].Equals(DBNull.Value) == true ? string.Empty : Convert.ToString(row["IsShowText"]);
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
    }
}
