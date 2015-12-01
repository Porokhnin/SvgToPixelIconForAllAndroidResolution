using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Svg;
using SvgToPixelIcon.Configuration.Models;
using Size = SvgToPixelIcon.Configuration.Models.Size;

namespace SvgToPixelIcon.Painter
{
    /// <summary>
    /// Клас художник
    /// </summary>
    public class SvgPainter
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        private readonly DirectoriesConfigInfo _configuration;

        /// <summary>
        /// Клас художник
        /// </summary>
        /// <param name="configuration">Конфигурация</param>
        public SvgPainter(DirectoriesConfigInfo configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Начать разукрашивать
        /// </summary>
        /// <param name="outputDirectoryPath">Выходная директория</param>
        public void StartPaint(String outputDirectoryPath)
        {
            PaintСonfiguration(outputDirectoryPath, _configuration);
        }
        /// <summary>
        /// Разукрасить конфигурацию
        /// </summary>
        /// <param name="outputDirectoryPath">Выходная директория</param>
        /// <param name="configuration">Конфигурация</param>
        private void PaintСonfiguration(String outputDirectoryPath, DirectoriesConfigInfo configuration)
        {
            foreach (var directoryConfigInfo in configuration.GetDirectories())
                PaintСonfiguration(outputDirectoryPath, directoryConfigInfo);

            foreach (var file in configuration.GetFiles())
                PaintAndSaveFile(outputDirectoryPath, file);
        }

        /// <summary>
        /// Разукрасить и сохраниь
        /// </summary>
        /// <param name="outputDirectoryPath">Выходная директория</param>
        /// <param name="fileConfigInfo">конфигурационная информация о файле</param>
        private void PaintAndSaveFile(String outputDirectoryPath, FilesConfigInfo fileConfigInfo)
        {
            var size = fileConfigInfo.ConfigInfo.Size;
            var sourceColor = fileConfigInfo.ConfigInfo.SourceColor;
            var color = fileConfigInfo.ConfigInfo.Color;
            var unitType = fileConfigInfo.ConfigInfo.SizeUnitType;
            try
            {
                var svgDocument = SvgDocument.Open(fileConfigInfo.FileInfo.FullName);
                ReColorDocument(ref svgDocument, sourceColor, color);

                if (unitType.Equals(SizeUnitType.Dp))
                {      
                    foreach (var densityInfo in AndroidDensities.DensityCollection)
                    {
                        ReSizeDocument(ref svgDocument, size, densityInfo.Density);
                        var documentSize = new Size(svgDocument.Width, svgDocument.Height);

                        var filePath = CreateDirectoryAndFilePath(outputDirectoryPath, fileConfigInfo, densityInfo);
                        SaveDocumentAsPngFile(ref svgDocument, filePath, ImageFormat.Png);

                        RaiseFilePainted(fileConfigInfo, Path.GetFileName(filePath), densityInfo.Key, documentSize);
                    }
                }
                else
                {
                    foreach (var densityInfo in AndroidDensities.DensityCollection)
                    {
                        ReSizeDocument(ref svgDocument, size, AndroidDensities.mdpi.Density);
                        var documentSize = new Size(svgDocument.Width, svgDocument.Height);

                        var filePath = CreateDirectoryAndFilePath(outputDirectoryPath, fileConfigInfo, densityInfo);
                        SaveDocumentAsPngFile(ref svgDocument, filePath, ImageFormat.Png);

                        RaiseFilePainted(fileConfigInfo, Path.GetFileName(filePath), densityInfo.Key, documentSize);
                    }
                }
            }
            catch (Exception ex)
            {
                RaiseFilePainted(ex);
            }
        }

        /// <summary>
        /// Создать директорию для файлов и сформировать путь для сохранения файла
        /// </summary>
        /// <param name="outputDirectoryPath">Выходная директория</param>
        /// <param name="fileConfigInfo">FilesConfigInfo</param>
        /// <param name="densityInfo">DensityInfo</param>
        /// <param name="fileExtension">Расширения</param>
        /// <returns></returns>
        private string CreateDirectoryAndFilePath(string outputDirectoryPath, FilesConfigInfo fileConfigInfo, DensityInfo densityInfo, string fileExtension="png")
        {
            string dimension = fileConfigInfo.ConfigInfo != null ? fileConfigInfo.ConfigInfo.Dimension : String.Empty;

            if(!Directory.Exists(Path.Combine(outputDirectoryPath, AndroidDensities.GetDensityDirectoryName(densityInfo.Key, dimension))))
            {
                Directory.CreateDirectory(Path.Combine(outputDirectoryPath, AndroidDensities.GetDensityDirectoryName(densityInfo.Key, dimension)));
            }
            return Path.Combine(outputDirectoryPath,
                                AndroidDensities.GetDensityDirectoryName(densityInfo.Key, dimension), String.Format("{0}.{1}", 
                                Path.GetFileNameWithoutExtension(fileConfigInfo.FileInfo.Name), fileExtension));
        }

        /// <summary>
        /// Сохранить документ как картинку
        /// </summary>
        /// <param name="svgDocument">Документ</param>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="imageFormat">Формат картинки</param>
        private void SaveDocumentAsPngFile(ref SvgDocument svgDocument, String filePath, ImageFormat imageFormat)
        {
            var bitmap = svgDocument.Draw();
            if (Equals(imageFormat, ImageFormat.Png))
            {
                bitmap.Save(filePath, imageFormat); 
            }
        }

        /// <summary>
        /// Изменить цвет у документа
        /// </summary>
        /// <param name="svgDocument">SvgDocument</param>
        /// <param name="sourceColor"></param>
        /// <param name="color">Новый цвет</param>
        private void ReColorDocument(ref SvgDocument svgDocument, System.Windows.Media.Color? sourceColor, System.Windows.Media.Color? color)
        {
            if (color.HasValue)
            {
                if (sourceColor.HasValue)
                {
                    foreach (SvgElement item in svgDocument.Children)
                    {
                        ChangeSvgElementColor(item, 
                            Color.FromArgb(sourceColor.Value.A, sourceColor.Value.R, sourceColor.Value.G, sourceColor.Value.B), 
                            Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B));
                    }
                }
                else
                {
                    foreach (SvgElement item in svgDocument.Children)
                    {
                        ChangeSvgElementColor(item, null, Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B));
                    }
                }
            }
        }

        /// <summary>
        /// Изменить цвет у элемента
        /// </summary>
        /// <param name="element">SvgElement</param>
        /// <param name="sourceColor">Реальный цвет</param>
        /// <param name="replaceColor">Новый цвет</param>
        private void ChangeSvgElementColor(SvgElement element, Color? sourceColor, Color replaceColor)
        {
            if (element is SvgPath)
            {
                if (sourceColor.HasValue)
                {
                    if (((element as SvgPath).Fill as SvgColourServer).Colour.ToArgb() == sourceColor.Value.ToArgb())
                    {
                        (element as SvgPath).Fill = new SvgColourServer(replaceColor);
                    }
                }
                else
                {
                    (element as SvgPath).Fill = new SvgColourServer(replaceColor);
                }
            }
 
            if (element.Children.Count > 0)
            {
                foreach (var item in element.Children)
                {
                    ChangeSvgElementColor(item, sourceColor, replaceColor);
                }
            }
        }

        /// <summary>
        /// Изменить размер документа
        /// </summary>
        /// <param name="document">Документ</param>
        /// <param name="size">Размер</param>
        /// <param name="density">Плотность</param>
        private void ReSizeDocument(ref SvgDocument document, Size? size, float density)
        {
            if (size.HasValue)
            {
                float pxWidth = size.Value.Width * density + 0.5f;
                float pxHeight = size.Value.Height * density + 0.5f;
                
                float documentScale;
                if (document.Height >= document.Width)
                {
                    documentScale = pxHeight / document.Height;
                }
                else
                {
                    documentScale = pxWidth / document.Width;
                }

                document.Width = (int) (document.Width*documentScale);
                document.Height = (int) (document.Height*documentScale);
            }
        }
        /// <summary>
        /// Событие о раскрасске файла
        /// </summary>
        public event EventHandler<PainterEventArgs> FilePainted;
        private void RaiseFilePainted(FilesConfigInfo fileConfigInfo, string fileName, string densityKey, Size documentSize)
        {
            RaiseFilePainted(new PainterEventArgs(fileConfigInfo, fileName, densityKey, documentSize));
        }
        private void RaiseFilePainted(Exception exception)
        {
            RaiseFilePainted(new PainterEventArgs(exception));
        }
        protected virtual void RaiseFilePainted(PainterEventArgs e)
        {
            EventHandler<PainterEventArgs> handler = FilePainted;
            if (handler != null) handler(this, e);
        }
    }
}
