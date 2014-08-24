   function LoadCss(cVarName, cStylePath) { var newStyle; var oHead = document.getElementsByTagName('head')[0]; var aLink = oHead.getElementsByTagName('link'); for (var i = 0; i < aLink.length; i++) { if (aLink[i].href == cStylePath) { newStyle = true; break; } } if (!newStyle) { newStyle = document.createElement('link'); newStyle.type = 'text/css'; newStyle.rel = 'stylesheet'; newStyle.href = cStylePath; oHead.appendChild(newStyle); } this.oo = cVarName; }

   function a_analysis_Click(analysis,t) {
            var p_tr = $(t).parent().parent();
            var sd_id = $(p_tr).find("input:checkbox").attr("id");
            if (analysis < 0) {
                return false;
            }
            if (sd_id=="") {
                return false;
            }
            datadetail.analysis_Click(sd_id, analysis, function (data) {
                var dataHtml_tr = data.value; //重新绑定这一行数据
                $(p_tr).html(dataHtml_tr);
            });
        }

        function a_hot_5_Click(hot,t) {
          var p_tr = $(t).parent().parent();
            var sd_id = $(p_tr).find("input:checkbox").attr("id");
            if (hot < 0) {
                return false;
            }
            if (sd_id=="") {
                return false;
            }
            datadetail.hot_Click(sd_id, hot, function (data) {
                var dataHtml_tr = data.value;
                //重新绑定这一行数据
                $(p_tr).html(dataHtml_tr);
            });
        }

        function a_attention_click(attention,t) {
            var p_tr = $(t).parent().parent();
            var sd_id = $(p_tr).find("input:checkbox").attr("id");
            if (attention < 0) {
                return false;
            } 
            if (sd_id == "") {
                return false;
            } 
            datadetail.attention_Click(sd_id, attention, function (data) {
                var dataHtml_tr = data.value;
                //重新绑定这一行数据
                $(p_tr).html(dataHtml_tr);
            });
        }

        