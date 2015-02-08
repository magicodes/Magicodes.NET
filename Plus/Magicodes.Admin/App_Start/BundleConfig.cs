using System.Web;
using System.Web.Optimization;

namespace Magicodes.Admin
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            //~/plus/Plugins/Magicodes.Admin
            var plusPath = "~" + Starter.PlusPath;
            //~/Magicodes.Admin/bundles
            var plusName = "~/" + Starter.PlusName + "/bundles";
            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle(plusName + "/Content/css").Include(
                      plusPath + "/Content/bootstrap.min.css",
                      plusPath + "/Content/animate.css",
                      plusPath + "/Content/style.css"));

            // Font Awesome icons
            //bundles.Add(new StyleBundle(plusName + "/font-awesome/css").Include(
            //          plusPath + "/fonts/font-awesome/css/font-awesome.min.css"));

            // jQuery
            bundles.Add(new ScriptBundle(plusName + "/jquery").Include(
                        plusPath + "/Scripts/jquery-2.1.1.min.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle(plusName + "/jqueryuiStyles").Include(
                        plusPath + "/Scripts/plugins/jquery-ui/jquery-ui.min.css"));

            // jQueryUI 
            bundles.Add(new StyleBundle(plusName + "/jqueryui").Include(
                        plusPath + "/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle(plusName + "/bootstrap").Include(
                      plusPath + "/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle(plusName + "/inspinia").Include(
                      plusPath + "/Scripts/plugins/metisMenu/metisMenu.min.js",
                      plusPath + "/Scripts/plugins/pace/pace.min.js",
                      plusPath + "/Scripts/app/inspinia.min.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle(plusName + "/skinConfig").Include(
                      plusPath + "/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle(plusName + "/plugins/slimScroll").Include(
                      plusPath + "/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // Peity
            bundles.Add(new ScriptBundle(plusName + "/plugins/peity").Include(
                      plusPath + "/Scripts/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle(plusName + "/plugins/videoResponsible").Include(
                      plusPath + "/Scripts/plugins/video/responsible-video.js"));

            // FancyBox gallery css styles
            bundles.Add(new StyleBundle(plusName + "/plugins/fancyboxStyles").Include(
                      plusPath + "/Scripts/plugins/fancybox/jquery.fancybox.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle(plusName + "/plugins/fancybox").Include(
                      plusPath + "/Scripts/plugins/fancybox/jquery.fancybox.js"));

            // Sparkline
            bundles.Add(new ScriptBundle(plusName + "/plugins/sparkline").Include(
                      plusPath + "/Scripts/plugins/sparkline/jquery.sparkline.min.js"));

            // Morriss chart css styles
            bundles.Add(new StyleBundle(plusName + "/plugins/morrisStyles").Include(
                      plusPath + "/Content/plugins/morris/morris-0.4.3.min.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle(plusName + "/plugins/morris").Include(
                      plusPath + "/Scripts/plugins/morris/raphael-2.1.0.min.js",
                      plusPath + "/Scripts/plugins/morris/morris.js"));

            // Flot chart
            bundles.Add(new ScriptBundle(plusName + "/plugins/flot").Include(
                      plusPath + "/Scripts/plugins/flot/jquery.flot.js",
                      plusPath + "/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      plusPath + "/Scripts/plugins/flot/jquery.flot.resize.js",
                      plusPath + "/Scripts/plugins/flot/jquery.flot.pie.js",
                      plusPath + "/Scripts/plugins/flot/jquery.flot.spline.js"));

            // Rickshaw chart
            bundles.Add(new ScriptBundle(plusName + "/plugins/rickshaw").Include(
                      plusPath + "/Scripts/plugins/rickshaw/vendor/d3.v3.js",
                      plusPath + "/Scripts/plugins/rickshaw/rickshaw.min.js"));

            // ChartJS chart
            bundles.Add(new ScriptBundle(plusName + "/plugins/chartJs").Include(
                      plusPath + "/Scripts/plugins/chartjs/Chart.min.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle(plusName + "/plugins/iCheckStyles").Include(
                      plusPath + "/Content/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle(plusName + "/plugins/iCheck").Include(
                      plusPath + "/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle(plusName + "/plugins/dataTablesStyles").Include(
                      plusPath + "/Content/plugins/dataTables/dataTables.bootstrap.css",
                      plusPath + "/Content/plugins/dataTables/dataTables.responsive.css",
                      plusPath + "/Content/plugins/dataTables/dataTables.tableTools.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle(plusName + "/plugins/dataTables").Include(
                      plusPath + "/Scripts/plugins/dataTables/jquery.dataTables.js",
                      plusPath + "/Scripts/plugins/dataTables/dataTables.bootstrap.js",
                      plusPath + "/Scripts/plugins/dataTables/dataTables.responsive.js",
                      plusPath + "/Scripts/plugins/dataTables/dataTables.tableTools.min.js"));

            // jeditable 
            bundles.Add(new ScriptBundle(plusName + "/plugins/jeditable").Include(
                      plusPath + "/Scripts/plugins/jeditable/jquery.jeditable.js"));

            // jqGrid styles
            bundles.Add(new StyleBundle(plusName + "/plugins/jqGridStyles").Include(
                      plusPath + "/Content/plugins/jqGrid/ui.jqgrid.css"));

            // jqGrid 
            bundles.Add(new ScriptBundle(plusName + "/plugins/jqGrid").Include(
                      plusPath + "/Scripts/plugins/jqGrid/i18n/grid.locale-en.js",
                      plusPath + "/Scripts/plugins/jqGrid/jquery.jqGrid.min.js"));

            // codeEditor styles
            bundles.Add(new StyleBundle(plusName + "/plugins/codeEditorStyles").Include(
                      plusPath + "/Content/plugins/codemirror/codemirror.css",
                      plusPath + "/Content/plugins/codemirror/ambiance.css"));

            // codeEditor 
            bundles.Add(new ScriptBundle(plusName + "/plugins/codeEditor").Include(
                      plusPath + "/Scripts/plugins/codemirror/codemirror.js",
                      plusPath + "/Scripts/plugins/codemirror/mode/javascript/javascript.js"));

            // codeEditor 
            bundles.Add(new ScriptBundle(plusName + "/plugins/nestable").Include(
                      plusPath + "/Scripts/plugins/nestable/jquery.nestable.js"));

            // validate 
            bundles.Add(new ScriptBundle(plusName + "/plugins/validate").Include(
                      plusPath + "/Scripts/plugins/validate/jquery.validate.min.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle(plusName + "/plugins/fullCalendarStyles").Include(
                      plusPath + "/Content/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar 
            bundles.Add(new ScriptBundle(plusName + "/plugins/fullCalendar").Include(
                      plusPath + "/Scripts/plugins/fullcalendar/moment.min.js",
                      plusPath + "/Scripts/plugins/fullcalendar/fullcalendar.min.js"));

            // vectorMap 
            bundles.Add(new ScriptBundle(plusName + "/plugins/vectorMap").Include(
                      plusPath + "/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      plusPath + "/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // ionRange styles
            bundles.Add(new StyleBundle(plusName + "/plugins/ionRangeStyles").Include(
                      plusPath + "/Content/plugins/ionRangeSlider/ion.rangeSlider.css",
                      plusPath + "/Content/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange 
            bundles.Add(new ScriptBundle(plusName + "/plugins/ionRange").Include(
                      plusPath + "/Scripts/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle(plusName + "/plugins/dataPickerStyles").Include(
                      plusPath + "/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle(plusName + "/plugins/dataPicker").Include(
                      plusPath + "/Scripts/plugins/datapicker/bootstrap-datepicker.js"));

            // nouiSlider styles
            bundles.Add(new StyleBundle(plusName + "/plugins/nouiSliderStyles").Include(
                      plusPath + "/Content/plugins/nouslider/jquery.nouislider.css"));

            // nouiSlider 
            bundles.Add(new ScriptBundle(plusName + "/plugins/nouiSlider").Include(
                      plusPath + "/Scripts/plugins/nouslider/jquery.nouislider.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle(plusName + "/plugins/jasnyBootstrapStyles").Include(
                      plusPath + "/Content/plugins/jasny/jasny-bootstrap.min.css"));

            // jasnyBootstrap 
            bundles.Add(new ScriptBundle(plusName + "/plugins/jasnyBootstrap").Include(
                      plusPath + "/Scripts/plugins/jasny/jasny-bootstrap.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle(plusName + "/plugins/switcheryStyles").Include(
                      plusPath + "/Content/plugins/switchery/switchery.css"));

            // switchery 
            bundles.Add(new ScriptBundle(plusName + "/plugins/switchery").Include(
                      plusPath + "/Scripts/plugins/switchery/switchery.js"));

            // chosen styles
            bundles.Add(new StyleBundle(plusName + "/plugins/chosenStyles").Include(
                      plusPath + "/Content/plugins/chosen/chosen.css"));

            // chosen 
            bundles.Add(new ScriptBundle(plusName + "/plugins/chosen").Include(
                      plusPath + "/Scripts/plugins/chosen/chosen.jquery.js"));

            // knob 
            bundles.Add(new ScriptBundle(plusName + "/plugins/knob").Include(
                      plusPath + "/Scripts/plugins/jsKnob/jquery.knob.js"));

            // wizardSteps styles
            bundles.Add(new StyleBundle(plusName + "/plugins/wizardStepsStyles").Include(
                      plusPath + "/Content/plugins/steps/jquery.steps.css"));

            // wizardSteps 
            bundles.Add(new ScriptBundle(plusName + "/plugins/wizardSteps").Include(
                      plusPath + "/Scripts/plugins/staps/jquery.steps.min.js"));

            // dropZone styles
            bundles.Add(new StyleBundle(plusName + "/plugins/dropZoneStyles").Include(
                      plusPath + "/Content/plugins/dropzone/basic.css",
                      plusPath + "/Content/plugins/dropzone/dropzone.css"));

            // dropZone 
            bundles.Add(new ScriptBundle(plusName + "/plugins/dropZone").Include(
                      plusPath + "/Scripts/plugins/dropzone/dropzone.js"));

            // summernote styles
            bundles.Add(new StyleBundle(plusName + "/plugins/summernoteStyles").Include(
                      plusPath + "/Content/plugins/summernote/summernote.css",
                      plusPath + "/Content/plugins/summernote/summernote-bs3.css"));

            // summernote 
            bundles.Add(new ScriptBundle(plusName + "/plugins/summernote").Include(
                      plusPath + "/Scripts/plugins/summernote/summernote.min.js"));

            // toastr notification 
            bundles.Add(new ScriptBundle(plusName + "/plugins/toastr").Include(
                      plusPath + "/Scripts/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle(plusName + "/plugins/toastrStyles").Include(
                      plusPath + "/Content/plugins/toastr/toastr.min.css"));

            // color picker
            bundles.Add(new ScriptBundle(plusName + "/plugins/colorpicker").Include(
                      plusPath + "/Scripts/plugins/colorpicker/bootstrap-colorpicker.min.js"));

            // color picker styles
            bundles.Add(new StyleBundle(plusName + "/plugins/colorpickerStyles").Include(
                      plusPath + "/Content/plugins/colorpicker/bootstrap-colorpicker.min.css"));

            // image cropper
            bundles.Add(new ScriptBundle(plusName + "/plugins/imagecropper").Include(
                      plusPath + "/Scripts/plugins/cropper/cropper.min.js"));

            // image cropper styles
            bundles.Add(new StyleBundle(plusName + "/plugins/imagecropperStyles").Include(
                      plusPath + "/Content/plugins/cropper/cropper.min.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
