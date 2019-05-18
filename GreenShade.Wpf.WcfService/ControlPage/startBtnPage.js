
$(function () {
    $('.simple-button').mouseup(function () {
      
        var forNowStr = (new Date()).getTime().toString();
       var id=$(this)[0].id
        console.log(forNowStr)
        console.log(id)      
        startDraw(forNowStr, id,function (data) {
                console.log(data)
            })
})


function startDraw(times,id,callBack) {
    $.ajax({
        url: "page?name=ip.json",//json文件位置
        type: "GET",//请求方式为get
        dataType: "json", //返回数据格式为json
        success: function(httpData) {//请求成功完成后要执行的方法
            // 请求本地的url路径
            console.log(httpData)
            var defHttp = httpData.rootUrl
            var url = `${defHttp}/Service/controller?btn_id=${id}`
            $.ajax({
                url: url,
                type: 'Get',
                dataType: 'json',
                success: function (data) {
                  
                    if(callBack){
                        callBack(data)
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
        }
    })
}
})
