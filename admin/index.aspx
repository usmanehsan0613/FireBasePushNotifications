<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="zu_push_notifications_admin_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body{ overflow: hidden;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-lg-12 col-md-12 col-xs-12  ">  
    <form id="form2" runat="server" data-toggle="validator" role="form">
            <div class="clear">&nbsp;</div>    
        <div class="clear">&nbsp;</div>    
        <div class="clear">&nbsp;</div>    
        <div class="form-group row">
              <label class="control-label col-sm-2" for="email">Title</label>
              <div class="col-sm-10">
                  <input required="" type="text" class="form-control" id="title" placeholder="Enter title">
              </div>
            </div>
             
             <div class="form-group row">
              <label class="control-label col-sm-2" for="email">Image URL</label>
              <div class="col-sm-10">
                <input type="text" class="form-control" id="url" placeholder="Enter absolute image URL">
              </div>
            </div>
             
               <div class="form-group row">
              <label class="control-label col-sm-2" for="email">Redirection URL</label>
              <div class="col-sm-10">
                <input type="text" class="form-control" id="redirectUrl" placeholder="e.g https://www.zu.ac.ae">
              </div>
            </div>
             
            <div class="form-group row">
              <label class="control-label col-sm-2" for="pwd">Message</label>
              <div class="col-sm-10">
                <textarea type="text" class="form-control" id="message" placeholder=" Body Message "></textarea>
              </div>
            </div>
             
               <div class="form-group row">
              <label class="control-label col-sm-2" for="email">Consumer KEY</label>
              <div class="col-sm-10">
                <input type="text" class="form-control" id="key" placeholder="e.g f7b1e1f5d8f27892cde188bc931645c9">
              </div>
            </div>

             <div class="form-group row">
                 <label class="col-sm-3 col-form-label">&nbsp;</label>
                 <div class="col-sm-6">
                     <div class="g-recaptcha" data-sitekey="6LdZWSETAAAAAIY0BbIQ2nJIUg6mFPokICBGlRi_"></div>

                 </div>
             </div>

             <div class="form-group row" id="error">
              <label class="control-label col-sm-2" for="email"></label>
              <div class="col-sm-10">
                  <label class="error" style="color: red;" id="lblResult"></label>
              </div>
            </div>
            <div class="form-group row">
              <div class="col-sm-offset-2 col-sm-10">
                  <button type="button" id="submit" class="btn btn-default">Submit</button>
              </div>
            </div>
        </form>
        </div>
     <script type="text/javascript">
         $("#submit").on("click", function (e) {
             e.preventDefault();
             alert('adfsadf');
             $('#btnSubmit').addClass('disabled');
             $('.disableForm').css('display', 'block');
             $('#lblResult').html("Processing... Please wait....");

             var notif = {};
             notif.title = $('#title').val();
             notif.url = $('#url').val();
             notif.redirectUrl = $('#redirectUrl').val();
             notif.message = $('#message').val();
             notif.key = $('#key').val();
             console.log(notif);
             notif.captcha = grecaptcha.getResponse();

             $.ajax({
                 type: "POST",
                 url: "index.aspx/procFrm",
                 // data: '{firstName: ' + JSON.stringify(firstName) + ', LastName: ' + JSON.stringify(LastName) + ', Email: ' + JSON.stringify(Email) + ', zuid: ' + JSON.stringify(zuid) + ', venue: ' + JSON.stringify(venue) + ', captcha: ' + JSON.stringify(captcha) + '}',
                 data: '{notif : ' + JSON.stringify(notif) + '}',
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {
                     console.log(response);
                     grecaptcha.reset();
                    

                 }
             });
             return false;

         });
     </script>
</asp:Content>

