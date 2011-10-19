namespace Crool.TextHighlight
{
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Utilities;
    using System.Windows.Media;

    [Export(typeof(EditorFormatDefinition))]
    [Name("MarkerFormatDefinition/HighlightReviewFormatDefinition")]
    [UserVisible(true)]
    internal class HighlightReviewFormatDefinition : MarkerFormatDefinition
    {
        public HighlightReviewFormatDefinition()
        {
            this.BackgroundColor = Colors.LightBlue;
            this.ForegroundColor = Colors.DarkBlue;
            this.DisplayName = "Highlight Word";
            this.ZOrder = 5;
        }
    }
}
