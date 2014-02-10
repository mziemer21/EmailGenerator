handleNewSection = function () {

    $(".hiddenTemp").hide();

    switch ($(this).val()) {
        case '1':
            $(".hiddenTemp").hide();
            break;
        case '2':
            $(".hiddenTemp").show();
            break;
        case '3':
            $(".hiddenTemp").hide();
            break;
        case '4':
            $(".hiddenTemp").hide();
            break;
        case '5':
            $(".hiddenTemp").hide();
            break;
    }
};

$(document).ready(function () {
    $("#date").datepicker({ dateFormat: 'mm/dd/yy' });
    $("#category").change(handleNewSection);

    // Run the event handler once now to ensure everything is as it should be
    handleNewSection.apply($("#category"));

    $(".leftColumn").on("click", ".move-up", function () {
        var curArticle = $(this).clostst(".article");
        var prevArticle = curArticle.prev();

        if ((curArticle.attr("id") != undefined) && (prevArticle.attr("id") != undefined)) {
            $.ajax({
                url: '/ArticlePosition',
                type: 'POST',
                data: { articleID: curArticle.attr("id"), articleID2: prevArticle.attr("id") },
                success: function (response) {
                    curArticle.fadeOut(400, function () {
                        curArticle.remove();
                        curArticle.insertBefore(prevArticle).fadeIn(400);
                    });
                }
            });
        }
        else {
            alert("This article cannot be moved up.");
        }
    });

    $(".leftColumn").on("click", ".move-down", function () {
        var curArticle = $(this).closest(".article");
        var nextArticle = curArticle.next();

        if ((curArticle.attr("id") != undefined) && (nextArticle.attr("id") != undefined)) {
            $.ajax({
                url: '/ArticlePosition',
                type: 'POST',
                data: { articleID: curArticle.attr("id"), articleID2: nextArticle.attr("id") },
                success: function (response) {
                    curArticle.fadeOut(400, function () {
                        curArticle.remove();
                        curArticle.insertAfter(nextArticle).fadeIn(400);
                    });
                }
            });
        }
        else {
            alert("This article cannot be moved down");
        }
    });
});