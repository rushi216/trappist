import { Component, OnInit, ViewChild } from "@angular/core";
import { MdDialog } from '@angular/material';
import { TestCreateDialogComponent } from "./test-create-dialog.component";
import { DeleteTestDialogComponent } from "./delete-test-dialog.component";
import { TestService } from "../tests.service";
import { Test } from "../tests.model";
import { ActivatedRoute, Router, provideRoutes } from '@angular/router';
import { Http } from "@angular/http";
import { TestSettingsComponent } from "../../tests/test-settings/test-settings.component";
import { TestSettingService } from "../testsetting.service";


@Component({
    moduleId: module.id,
    selector: "tests-dashboard",
    templateUrl: "tests-dashboard.html"
})

export class TestsDashboardComponent {


    Tests: Test[] = new Array<Test>();
    


    constructor(public dialog: MdDialog, private testService: TestService) {

        this.getAllTests();

    }

    //Get All The Tests From Server
    getAllTests() {
        this.testService.getTests().subscribe((response) => { this.Tests = (response), console.log(this.Tests) });


    }

    // Open Create Test Dialog
    constructor(public dialog: MdDialog) { }
    createTestDialog() {
        this.dialog.open(TestCreateDialogComponent);
    }
}
export class Test
{
    TestName: string;
}
@Component({
    moduleId: module.id,
    selector: 'test-create-dialog',
    templateUrl: "test-create-dialog.html"
})
export class TestCreateDialogComponent {
    testDashboard: TestsDashboardComponent;
    test: Test = new Test;
    testCreateResponse: any;
    t_list: TestsDashboardComponent[] = new Array<TestsDashboardComponent>();
    constructor(public http: Http, private route: ActivatedRoute, public dialog: MdDialog)
    {
    }
    /*
    this method is used to add a new test
    parameter passed is name of the test
    */
    AddTest(testName: string)
    {
        this.test.TestName = testName;
        this.http.post("api/tests", this.test).subscribe((response) => {
            if (response.ok)
                this.testCreateResponse = (response);
        }); 
        this.dialog.closeAll();       
    }
}

