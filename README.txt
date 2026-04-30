=============================================================
  SNIPE IT TEST AUTOMATION - ANDITO PRANANDYA RAMADHAN
=============================================================

Hi, this is the readme for my Snipe-IT automation project.

This file contains the information about what your PC has to have before running the project, how to run it, and what each test script does.

-------------------------------------------------------------
  PREREQUISITES
-------------------------------------------------------------

Before you can run this project, please make sure your PC has the following installed:

1. .NET 8 SDK

2. Windows PowerShell

3. Git (only if you want to clone from my repository)


-------------------------------------------------------------
  FIRST TIME SETUP
-------------------------------------------------------------

Step 1 - Restore the packages:
   Open Command Prompt, cd to the project folder, and run:

   dotnet restore

   This will download all the necessary libraries  from the internet.

Step 2 - Build the project:

   dotnet build

   This compiles the code.

Step 3 - Install the Playwright browser (Chromium):

   Run this command:

   powershell -Command ".\bin\Debug\net8.0\playwright.ps1 install chromium"

   and wait for the Chromium to get installed.


-------------------------------------------------------------
  HOW TO RUN THE TESTS
-------------------------------------------------------------

After the setup is done, you can run the tests with this command:

   set HEADED=1& dotnet test --logger "console;verbosity=detailed"

The "HEADED=1" part makes the browser window visible, so you can watch the step by step test process.

The "--logger" part makes the step-by-step output visible in the terminal.

After the test is done, you will see the result, alongside the test steps that the scripts have taken.

Example:

NUnit Adapter 4.6.0.0: Test execution started
Running all tests in C:\Users\ARamadhan\Documents\SnipeITTest\bin\Debug\net8.0\SnipeITTest.dll
   NUnit3TestExecutor discovered 1 of 1 NUnit test cases using Current Discovery mode, Non-Explicit run
[09:56:04] Test started
[09:56:04] Asset tag generated: TEST-1777506960256
[09:56:10] Step 1 PASSED: Login successful
[09:56:10]   URL after login: https://demo.snipeitapp.com/
[09:56:16] Step 2 PASSED: Asset created successfully
[09:56:16]   Asset Tag:     TEST-1777506960256
[09:56:16]   Model:         MacBook Pro 13"
[09:56:16]   Status:        Ready to Deploy
[09:56:16]   Checked Out To: Felton Anderson (uchamplin) - #26790
[09:56:16]   URL after save: https://demo.snipeitapp.com/hardware
[09:56:22] Step 3 PASSED: Asset found in the assets list
[09:56:22]   Searched for: TEST-1777506960256
[09:56:24] Step 4 PASSED: Asset detail page validated
[09:56:24]   Asset Tag:      TEST-1777506960256
[09:56:24]   Model:          Macbook Pro 13"
[09:56:24]   Breadcrumb:     #TEST-1777506960256 - Macbook Pro 13"
[09:56:24]   Status:         Ready to Deploy Deployed   Felton Anderson
[09:56:24]   Checked Out To: Felton Anderson
[09:56:24]   Asset URL:      https://demo.snipeitapp.com/hardware/2612
[09:56:26] Step 5 PASSED: History tab validated
[09:56:26]   History contains: create action
[09:56:26]   History contains: checkout action
[09:56:26] All steps passed.
Log saved to: C:\Users\ARamadhan\Documents\SnipeITTest\bin\Debug\net8.0\Logs\Test Run - 30042026 995604.txt

NUnit Adapter 4.6.0.0: Test execution complete
  Passed FullAssetFlow [26 s]
  Standard Output Messages:
 [09:56:04] Test started
 [09:56:04] Asset tag generated: TEST-1777506960256
 [09:56:10] Step 1 PASSED: Login successful
 [09:56:10]   URL after login: https://demo.snipeitapp.com/
 [09:56:16] Step 2 PASSED: Asset created successfully
 [09:56:16]   Asset Tag:     TEST-1777506960256
 [09:56:16]   Model:         MacBook Pro 13"
 [09:56:16]   Status:        Ready to Deploy
 [09:56:16]   Checked Out To: Felton Anderson (uchamplin) - #26790
 [09:56:16]   URL after save: https://demo.snipeitapp.com/hardware
 [09:56:22] Step 3 PASSED: Asset found in the assets list
 [09:56:22]   Searched for: TEST-1777506960256
 [09:56:24] Step 4 PASSED: Asset detail page validated
 [09:56:24]   Asset Tag:      TEST-1777506960256
 [09:56:24]   Model:          Macbook Pro 13"
 [09:56:24]   Breadcrumb:     #TEST-1777506960256 - Macbook Pro 13"
 [09:56:24]   Status:         Ready to Deploy Deployed   Felton Anderson
 [09:56:24]   Checked Out To: Felton Anderson
 [09:56:24]   Asset URL:      https://demo.snipeitapp.com/hardware/2612
 [09:56:26] Step 5 PASSED: History tab validated
 [09:56:26]   History contains: create action
 [09:56:26]   History contains: checkout action
 [09:56:26] All steps passed.
 Log saved to: C:\Users\ARamadhan\Documents\SnipeITTest\bin\Debug\net8.0\Logs\Test Run - 30042026 995604.txt

Test Run Successful.
Total tests: 1
     Passed: 1
 Total time: 28.3354 Seconds


-------------------------------------------------------------
  LOGGING
-------------------------------------------------------------

After every run, a log file is automatically created in:

   bin\Debug\net8.0\Logs\

The file name format is "Test Run - DDMMYYYY HhMmSs":

Example: Test Run - 30042026 143022.txt

Each run creates a new file so old logs are not overwritten.

The log contains details like the asset tag, model, status, checked out user, and which steps passed.


-------------------------------------------------------------
  PROJECT FILE STRUCTURE
-------------------------------------------------------------

SnipeITTest/
  Helpers/
    TestLogger.cs         Handles creating and saving log files
  Pages/
    LoginPage.cs          Interactions with the login page
    CreateAssetPage.cs    Interactions with the create asset form
    AssetsListPage.cs     Interactions with the assets list page
    AssetDetailPage.cs    Interactions with the asset detail page
  AssetTests.cs           The main test file with all 5 steps
  LoginData.txt           Stores the login credentials to be utilized by AssetTests.cs
  SnipeItTest.csproj      Project configuration and dependencies
  bin/
    Debug/
        net8.0/
            Logs/         Contains the log files


-------------------------------------------------------------
  TEST WORKFLOW
-------------------------------------------------------------

- AssetTests.cs -

This is the main test file. It contains one test method called FullAssetFlow which runs all 5 tests in order, by calling the page-specific methods.

  SetUp() - runs before the test
  - Reads credentials from LoginData.txt
  - Creates instances of all page objects and the logger

  TearDown() - runs after the test (pass or fail)
  - Saves the log file

  FullAssetFlow() - the main test
  - Step 1: Login and verify the success message
  - Step 2: Create a MacBook Pro 13" asset with Ready to Deploy status, checked out to a random user
  - Step 3: Go to the assets list and search for the newly created asset to confirm it exists
  - Step 4: Open the asset detail page and validate the asset tag, model, status, and checked out user
  - Step 5: Open the History tab and confirm it contains both a "create" and a "checkout" action entry

Each step logs its result and key details to the log file.
