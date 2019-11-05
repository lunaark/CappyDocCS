using System;
using System.Drawing;
using System.IO;

using Xceed.Document.NET;
using Xceed.Words.NET;

namespace CappyDocCS
{
    public class DocumentBuilder
    {
        public void BuildDocument(string[] ScriptItems)
        {
            string FullDocFileName = Cappy.FolderName + @"\" + Cappy.Prefix + Cappy.FieldSeperator + Cappy.GetSaveTime() + Cappy.FieldSeperator + Cappy.DocExtension;

            using (DocX document = DocX.Create(FullDocFileName))
            {
                foreach (string ScriptItem in ScriptItems)
                {
                    if (ScriptItem != null)
                    {
                        // instantiate some
                        // TODO: this loop contains alot of copy paste and hackiness because im tired and shit, please clean it up
                        string[] Fields = ScriptItem.Split(';');

                        string ButtonAction = String.Empty;
                        string ButtonClicked = String.Empty;
                        string WindowText = String.Empty;
                        string FullFileName = String.Empty;
                        string FocusFileName = String.Empty;

                        if (Fields.Length == 4)
                        {
                            ButtonAction = Fields[0];
                            ButtonClicked = Fields[1];
                            WindowText = Fields[2];
                            FullFileName = Fields[3];
                            FocusFileName = Fields[4];
                        }
                        else if (Fields.Length == 2)
                        {
                            ButtonAction = Fields[0];
                            ButtonClicked = Fields[1];
                            FullFileName = Fields[2];
                        }

                        string ParagraphText;

                        if (Fields.Length == 4)
                        {
                            // because alot of things in windows have weird window names, or you perform more advanced actions on them then click, let's define some things to make our documents nicer
                            if (!String.IsNullOrEmpty(WindowText))
                            {
                                // if string is not empty, check it for aforementioned jank
                                switch (WindowText)
                                {
                                    case "System Promoted Notification Area":
                                        WindowText = "Notification Tray";
                                        break;

                                    case "New notification":
                                        WindowText = "Notification";
                                        break;

                                    case "Overflow Notification Area":
                                        WindowText = "System Tray Icon";
                                        break;

                                    case "Running applications":
                                        WindowText = "Taskbar Icon";
                                        break;

                                    case "Start":
                                        WindowText = "Start Menu";
                                        break;
                                }
                                ParagraphText = ButtonAction + " " + WindowText;
                            }
                            else
                            {
                                // if we can't determine what was clicked, resort to this.
                                ParagraphText = ButtonAction + " << FILL IN MISSING TEXT >>";
                            }

                            // create our paragraph to work with
                            Paragraph p = document.InsertParagraph();

                            // get bitmaps of the desired images
                            System.Drawing.Image FullCaptureImage = GetImage(FullFileName);
                            System.Drawing.Image FocusCaptureImage = GetImage(FocusFileName);

                            // instantiate memory streams for writing our bitmaps to
                            MemoryStream FullCaptureStream = new MemoryStream();
                            MemoryStream FocusCaptureStream = new MemoryStream();

                            // save our bitmaps into the memory stream
                            FullCaptureImage.Save(FullCaptureStream, FullCaptureImage.RawFormat);
                            FocusCaptureImage.Save(FocusCaptureStream, FocusCaptureImage.RawFormat);

                            // set position in memory stream
                            FullCaptureStream.Seek(0, SeekOrigin.Begin);
                            FocusCaptureStream.Seek(0, SeekOrigin.Begin);

                            // create the base image to work with, and add it to our document object for later
                            Xceed.Document.NET.Image FullCapture = document.AddImage(FullCaptureStream);
                            Xceed.Document.NET.Image FocusCapture = document.AddImage(FocusCaptureStream);

                            // convert image to picture usable in a document
                            Picture FullCapturePic = FullCapture.CreatePicture();
                            Picture FocusCapturePic = FocusCapture.CreatePicture();

                            // because screenshots will be a constant size, simply set a scalar to our desired resolution. we can also use this for calculating the focus size.
                            const int imgWidth = 512;
                            const int imgHeight = 288;

                            // apply aforementioned scalar
                            FullCapturePic.Width = imgWidth;
                            FullCapturePic.Height = imgHeight;

                            // create focused screenshot size
                            Size focusbbox = new Size(imgWidth, imgHeight);

                            // get the scaled version of focused image
                            Size focusSize = ExpandToBound(FocusCaptureImage.Size, focusbbox);
                            FocusCapturePic.Width = focusSize.Width;
                            FocusCapturePic.Height = focusSize.Height;

                            // insert bullet list
                            var list = document.AddList(listType: ListItemType.Numbered, continueNumbering: true);
                            document.AddListItem(list, ParagraphText, 0, listType: ListItemType.Numbered);

                            // insert images
                            p.InsertListBeforeSelf(list);
                            p.AppendLine().AppendPicture(FullCapturePic);
                            p.AppendLine(); // space the images
                            p.AppendLine().AppendPicture(FocusCapturePic);
                            p.AppendLine(); // leave a space for writing

                            p.InsertPageBreakAfterSelf();

                            // release unneeded resources
                            FullCaptureImage.Dispose();
                            FullCaptureStream.Dispose();
                            FocusCaptureImage.Dispose();
                            FocusCaptureStream.Dispose();
                        }
                        if (Fields.Length == 2)
                        {
                            ParagraphText = ButtonAction + " " + ButtonClicked;

                            // create our paragraph to work with
                            Paragraph p = document.InsertParagraph();

                            // get bitmaps of the desired images
                            System.Drawing.Image FullCaptureImage = GetImage(FullFileName);

                            // instantiate memory streams for writing our bitmaps to
                            MemoryStream FullCaptureStream = new MemoryStream();

                            // save our bitmaps into the memory stream
                            FullCaptureImage.Save(FullCaptureStream, FullCaptureImage.RawFormat);

                            // set position in memory stream
                            FullCaptureStream.Seek(0, SeekOrigin.Begin);

                            // create the base image to work with, and add it to our document object for later
                            Xceed.Document.NET.Image FullCapture = document.AddImage(FullCaptureStream);

                            // convert image to picture usable in a document
                            Picture FullCapturePic = FullCapture.CreatePicture();

                            // because screenshots will be a constant size, simply set a scalar to our desired resolution. we can also use this for calculating the focus size.
                            const int imgWidth = 512;
                            const int imgHeight = 288;

                            // apply aforementioned scalar
                            FullCapturePic.Width = imgWidth;
                            FullCapturePic.Height = imgHeight;

                            // insert bullet list
                            var list = document.AddList(listType: ListItemType.Numbered, continueNumbering: true);
                            document.AddListItem(list, ParagraphText, 0, listType: ListItemType.Numbered);

                            // insert images
                            p.InsertListBeforeSelf(list);
                            p.AppendLine().AppendPicture(FullCapturePic);
                            p.AppendLine(); // space the images

                            // leave a space for writing
                            p.InsertPageBreakAfterSelf();

                            // release unneeded resources
                            FullCaptureImage.Dispose();
                            FullCaptureStream.Dispose();
                        }
                    }
                }
                document.Save();
            }
        }

        public System.Drawing.Image GetImage(string BitmapFileName)
        {
            System.Drawing.Image myImg = System.Drawing.Image.FromFile(BitmapFileName);
            return myImg;
        }

        private static Size ExpandToBound(Size image, Size boundingBox)
        {
            double widthScale = 0, heightScale = 0;
            if (image.Width != 0)
                widthScale = (double)boundingBox.Width / (double)image.Width;
            if (image.Height != 0)
                heightScale = (double)boundingBox.Height / (double)image.Height;

            double scale = Math.Min(widthScale, heightScale);

            Size result = new Size((int)(image.Width * scale),
                                (int)(image.Height * scale));
            return result;
        }
    }
}