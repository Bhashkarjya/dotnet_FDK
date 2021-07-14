for i in {1..368}
do 
    echo '{"Name":"Daniel","Age":18,"Subjects":[]}' | fn invoke dotnetFDK fdk
done