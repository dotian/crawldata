//        function StringBuilder(args) {
//            this._strings = [];
//            this._isBuild = false; //�Ƿ񴴽�
//            this._string = "";    //��������ַ���
//            for (var i = 0; i < arguments.length; i++) {
//                this._strings.push(arguments[0]);
//            }
//        }

//        StringBuilder.prototype.append = function (str) {
//            this._strings.push(str);
//            this._isBuild = false;
//            return this;
//        }
//        StringBuilder.prototype.toString = function () {
//            if (!this._isBuild) {
//                this._string = this._strings.join("");
//            }
//            return this._string;
//        }

//        //���س���
//        StringBuilder.prototype.length = function () {
//            if (!this._isBuild) {
//                this._string = this._strings.join("");
//            }
//            return this._string.length;
//        }



//        // ɾ����󼸸��ַ�
//        StringBuilder.prototype.del = function (lastNum) {
//            var len = this.length();
//            var str = this.toString();
//            this._string = str.slice(0, len - lastNum);
//            this._strings = [];
//            this._strings.push(this._string);
//            this.isBuild = true;
//            return this;
//        }



//        Date.prototype.format = function (format) {
//            var o = {
//                "M+": this.getMonth() + 1, //month 
//                "d+": this.getDate(), //day 
//                "H+": this.getHours(), //hour 
//                "m+": this.getMinutes(), //minute 
//                "s+": this.getSeconds(), //second 
//                "q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
//                "S": this.getMilliseconds() //millisecond 
//            }
//            if (/(y+)/.test(format)) {
//                format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
//            }

//            for (var k in o) {
//                if (new RegExp("(" + k + ")").test(format)) {
//                    format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
//                }
//            }
//            return format;
//        }

//        function DeleteDataById(siteDataId) {
//            var rowCount = 0;
//            $("#tb_dataList tr").each(function () {

//                rowCount++;
//            });
//            alert(rowCount);


//            $.post("ashxHelp/HandlerDeleteDataById.ashx", { sd_id: siteDataId }, function (b) {
//                if (b == "True") {
//                    alert("ɾ���ɹ�!");

//                } else {
//                    alert("ɾ��ʧ��!");
//                }

//                // �� ��һ�У���һ����Ԫ�� ��ʼ�滻�ı�

//                // var currentIndex = $("#AspNetPager1_input").val();
//                //alert(currentIndex);

//                var m = $("td[valign='bottom'][align='right']").text();

//                var strMatch = m.match(/\d+/g);
//                var currentIndex = strMatch[0]; //�õ���ǰҳ

//                var startTime = $("#txt_timeStart").val();
//                var endTime = $("#txt_timeEnd").val();

//                admin_ProjectDataDetail.ActionLaterBind(siteDataId, currentIndex, startTime, endTime, function (b) {
//                    var obj = eval(b.value);
//                    var i = -1;
//                    $("#tb_dataList tr").each(function () {
//                        if (i >= 0 && obj[i]!=null) {
//                            var sbreHtml = new StringBuilder();

//                            sbreHtml.append("<td><input type='checkbox' id='" + obj[i].SD_Id + "' value='" + obj[i].SD_Id + "' onclick='ck_one_click(this.id);' /></td>");

//                            if (obj[i].Title.length > 13) {
//                                obj[i].Title = obj[i].Title.substr(0, 10) + "...";
//                            }

//                            sbreHtml.append("<td  class='style_content'>" + obj[i].Title + "<span style='color: #875805;'>&nbsp;ͬ</span><br />��ǩ:</td>");

//                            if (obj[i].Content.length > 18) {
//                                obj[i].Content = obj[i].Content.substr(0, 15) + "...";
//                            }

//                            sbreHtml.append("<td  class='style_content'>" + obj[i].Content + "</td>");

//                            sbreHtml.append("<td>" + new Date(Date.parse(obj[i].CreateDate)).format('yyyy-MM-dd HH:mm:ss') + "</td>");
//                            sbreHtml.append("<td  style='padding-left: 20px;'>");
//                            sbreHtml.append("<a href='#'>��</a> <a href='#'>��</a> <a href='#'> ��</a> <a href='#'>��</a><br />");
//                            sbreHtml.append("<a href='#'>��ǩ</a> <a href='#'>��ע</a> <a href='#'>���</a> <a href='#' onclick='DeleteDataById(" + obj[i].SD_Id + ");'>ɾ��</a>");
//                            sbreHtml.append("</td>");
//                            sbreHtml.append("<td  align='center'><a href='" + obj[i].SrcUrl + "' target='_blank'>��</a></td>");
//                            sbreHtml.append("<td  align='center'>" + obj[i].SiteName + "</td>");
//                            sbreHtml.append("<td>���ڴ���</td>");
//                            $(this).html(sbreHtml.toString());
//                        } else {
//                            $(this).html("");
//                        }

//                        i++;
//                    });
//                });
//            });
//        }