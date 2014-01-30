handelNewSection = function () {

    switch ($(this).val()) {
        case '1':
            $(".hide").hide();
            break;
        case '2':
            $(".hide").show();
            break;
        case '3':
            $(".hide").hide();
            break;
        case '4':
            $(".hide").hide();
            break;
        case '5':
            $(".hide").hide();
            break;
    }
};

$(document).ready(function () {
    $("#date").datepicker();
    $("#category").change(handelNewSection);

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