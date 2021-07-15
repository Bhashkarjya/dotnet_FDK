for i in {1..2}
do 
    echo '{"Name":"Daniel","Age":18,"Subjects":["OS","CN"]}' | fn invoke dotnetFDK fdk 
done