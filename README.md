# Caretek Corporation Interview

Part A

Task is to send an email with attachment using Gmail 

 

The system will generate a new file using Memory Stream with name format "MMDDYYYY_HHMMSS.txt". For example the file name will be "01232022_153524.txt". The file should be created in memory only using Memory Stream and should not be saved on hard drive. 

Then System will put some random text in that file in the following way 

Name: [Some Random Text of length between 3- 20] space [Some Random Text of length between 3- 20].   

Age: Some Random number between 20 and 100. 

 

 For example 

Name:   Rene Davis 

Age: 54. 

 

 

Then send it through Gmail with following subject 

PatientReport_ + file name 

For example subject will be “PatientReport_01232022_153524.txt" 


Part B

Task is to read email that we have just sent. 

 

The system will connect to a mail server (Gmail) and see if any new email with subject format described above has came or not 

For example the format of the subject of the mail "PatientReport_01232022_153524.txt" 

Any email with subject starting from "PatientReport_" should be checked. 

 

If such an email is found then it should read the attachment of that email and extract Name and Age. 


