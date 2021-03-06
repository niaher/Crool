﻿namespace Crool.TextHighlight
{
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Operations;
    using Microsoft.VisualStudio.Text.Tagging;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(IViewTaggerProvider))]
    [ContentType("text")]
    [TagType(typeof(TextMarkerTag))]
    internal class HighlightReviewTaggerProvider : IViewTaggerProvider
    {
        [Import]
        internal ITextSearchService TextSearchService { get; set; }

        [Import]
        internal ITextStructureNavigatorSelectorService TextStructureNavigatorSelector { get; set; }

        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            // provide highlighting only on the top buffer
            if (textView.TextBuffer != buffer) return null;

            ITextStructureNavigator textStructureNavigator = this.TextStructureNavigatorSelector.GetTextStructureNavigator(buffer);

            return new HighlightReviewTagger(textView, buffer, this.TextSearchService, textStructureNavigator) as ITagger<T>;
        }
    }
}
