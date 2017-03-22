import { CodingLanguage } from "./coding.language.model";

export class ProgrammingQuestion {
    questionDetail: string;
    questionType: number;
    difficultyLevel: number;
    categoryId: number;
    checkCodeComplexity: boolean;
    checkTimeComplexity: boolean;
    runBasicTestCase: boolean;
    runCornerTestCase: boolean;
    runNecessaryTestCase: boolean;
    languageList: CodingLanguage[] = new Array<CodingLanguage>();
}