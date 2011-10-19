namespace Crool.TextHighlight
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading;
    using System.Windows.Media;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Classification;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Operations;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    internal class HighlightReviewTag : TextMarkerTag
    {
        public HighlightReviewTag()
            : base("MarkerFormatDefinition/HighlightReviewFormatDefinition")
        {
        }
    }
}
