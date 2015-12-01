using System;
using SvgToPixelIcon.Configuration.Models;

namespace SvgToPixelIcon.Painter
{
    public class PainterEventArgs : EventArgs
    {
        /// <summary>
        /// Конфигурационая информация для файла
        /// </summary>
        public FilesConfigInfo FileConfigInfo { get; private set; }

        /// <summary>
        /// Имя файла картинки
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Ключ плотности
        /// </summary>
        public string DensityKey { get; set; }

        /// <summary>
        /// Размер документа
        /// </summary>
        public Size? DocumentSize { get; set; }

        /// <summary>
        /// Ошибка
        /// </summary>
        public Exception Exception { get; private set; }

        public PainterEventArgs(Exception exception)
            : this(null, null, null, null, exception)
        {

        }
        public PainterEventArgs(FilesConfigInfo fileConfigInfo, string fileName, string densityKey, Size documentSize)
            : this(fileConfigInfo, fileName, densityKey, documentSize, null)
        {

        }
        private PainterEventArgs(FilesConfigInfo fileConfigInfo, string fileName, string densityKey, Size? documentSize, Exception exception)
        {
            FileConfigInfo = fileConfigInfo;
            FileName = fileName;
            DensityKey = densityKey;
            DocumentSize = documentSize;
            Exception = exception;
        }
    }
}
