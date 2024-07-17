# Will create a json representation of the debug launch arguments for a specified dll file
# Usage: ./gen_debug_args.sh <path_to_dll> <cwd> <args>

# Check if the correct number of arguments were passed
if [ "$#" -ne 3 ]; then
    echo "Usage: ./gen_debug_args.sh <path_to_dll> <cwd> <args>"
    exit 1
fi

# Get absolute path to the dll
dll_path=$(realpath $1)

# Get absolute path to the cwd
cwd=$(realpath $2)

# Create the json representation of the debug launch arguments
launch_args="{\"type\":\"coreclr\",\"request\":\"launch\",\"program\":\"$dll_path\",\"cwd\":\"$cwd\",\"just_my_code\":false,\"args\":\"$3\"}"

# Url encode the json representation
launch_args=$(echo $launch_args | jq -r @uri)

# Print the url encoded json representation
echo $launch_args