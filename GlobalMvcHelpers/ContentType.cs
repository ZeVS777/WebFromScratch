namespace GlobalMvcHelpers
{
    /// <summary>
    /// Перечень типов интернет медиа.
    /// </summary>
    public static class ContentType
    {
        /// <summary>
        /// Atom feeds.
        /// </summary>
        public const string Atom = "application/atom+xml";
        /// <summary>
        /// HTML; RFC 2854.
        /// </summary>
        public const string Html = "text/html";
        /// <summary>
        /// GIF изображение; RFC 2045 и RFC 2046.
        /// </summary>
        public const string Gif = "image/gif";
        /// <summary>
        /// JPEG JFIF изображение; RFC 2045 и RFC 2046.
        /// </summary>
        public const string Jpg = "image/jpeg";
        /// <summary>
        /// JavaScript Object Notation JSON; RFC 4627.
        /// </summary>
        public const string Json = "application/json";
        /// <summary>
        /// Portable Network Graphics; RFC 2083.
        /// </summary>
        public const string Png = "image/png";
        /// <summary>
        /// Текстовые данные; RFC 2046 и RFC 3676.
        /// </summary>
        public const string Text = "text/plain";
        /// <summary>
        /// Extensible Markup Language; RFC 3023
        /// </summary>
        public const string Xml = "application/xml";
    }
}