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
import { Response } from "../tests.model";

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

    // get All The Tests From Server
    getAllTests() {
        this.testService.getTests().subscribe((response) => { this.Tests = (response), console.log(this.Tests) });
    }
    // open Create Test Dialog
    createTestDialog() {
        this.dialog.open(TestCreateDialogComponent);
    }
}



@Component({
    moduleId: module.id,
    selector: 'test-create-dialog',
    templateUrl: "test-create-dialog.html"
})

export class TestCreateDialogComponent {
    private errorMessage: boolean;
    testCreateResponse: any;
    teststring: string;
    test: Test = new Test;
    t_list: TestsDashboardComponent[] = new Array<TestsDashboardComponent>();
    constructor(private route: ActivatedRoute, public dialog: MdDialog, public testDashboard: TestsDashboardComponent, private testService: TestService) {
    }
    /**
    this method is used to add a new test
    parameter passed is name of the test
    **/
    AddTest(testNameRef: string) {
        this.test.testName = testNameRef;

        this.testService.addTests("api/tests", this.test).subscribe((response: Response) => {
            if (response.responseValue == true)
            {
                this.testCreateResponse = (response)
                this.errorMessage = false;
            }
            else           
                this.errorMessage = true;                     
        });
        this.dialog.closeAll();
        this.testDashboard.Tests.push(this.testCreateResponse);
    }
}

