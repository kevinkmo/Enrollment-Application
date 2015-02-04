define(['Models/NewModel'], function (NewModel) {
    function NewViewModel() {
        var self = this;

        this.Load = function (id) {
            var obj = new NewModel();

            // Because the Load() is a async call (asynchronous), we'll need to use
            // the callback approach to handle the data after data is loaded.
           obj.Load(id, function (result) {

               console.log('test = ', result);
               var viewModel = {
                   id: ko.observable(result.CourseId),
                   title: ko.observable(result.Title),
                   description: ko.observable(result.Description),
                   prereq: ko.observable(result.prereq),
                   unit: ko.observable(result.unit),
                   level: ko.observable(result.CourseLevel),
                   update: function () {
                       self.Update(this);
                   }
               }

               ko.applyBindings(viewModel, document.getElementById("divCourse"));
           });
        };

        this.Update = function (viewModel) {
            var obj = new NewModel();

            // convert the viewModel to same structure as PLAdmin model (presentation layer model)
            var data = {
                CourseId: viewModel.id(),
                Title: viewModel.title(),
                CourseLevel: viewModel.level(),
                Description: viewModel.description(),
                prereq: viewModel.prereq(),
                unit: viewModel.unit(),

            };

           obj.Update(data, function (message) {
                $('#divMessage').html(message);
            });

        };
    }
    return NewViewModel;
}
);