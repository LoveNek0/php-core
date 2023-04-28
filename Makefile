PROJECT = $(shell basename $(CURDIR))

SRC_DIR = src
OUT_DIR = out
BINARY_DIR = $(OUT_DIR)/bin
LIBRARY_DIR = $(OUT_DIR)/lib

OUTPUT_BIN = $(BINARY_DIR)/$(PROJECT).exe
OUTPUT_LIB = $(LIBRARY_DIR)/$(PROJECT).dll

SOURCE_FILES = $(shell find $(SRC_DIR) -name "*.cs")

clean:
	@echo "Cleaning output directory..."
	@rm -rf $(OUT_DIR)

compile-bin: clean
	@echo "Compiling source files to binary..."
	@mkdir -p $(SRC_DIR) $(OUT_DIR) $(BINARY_DIR)
	@mcs -out:$(OUTPUT_BIN) $(SOURCE_FILES)
	@echo "Compilation completed!"
	@echo "Output file: $(OUTPUT_BIN)"

compile-lib: clean
	@echo "Compiling source files to library..."
	@mkdir -p $(SRC_DIR) $(OUT_DIR) $(LIBRARY_DIR)
	@mcs -out:$(OUTPUT_LIB) -t:library $(SOURCE_FILES)
	@echo "Compilation completed!"
	@echo "Output file: $(OUTPUT_LIB)"
	
ifeq (run, $(firstword $(MAKECMDGOALS)))
	 runargs := $(wordlist 2, $(words $(MAKECMDGOALS)), $(MAKECMDGOALS))
	 $(eval $(runargs):;@true)
endif
run: compile-bin
	@echo "Running \"$(OUTPUT_BIN)\"..."
	@mono $(OUTPUT_BIN) $(runargs)
	@echo "Program exited with code $$?"

dump-lib: compile-lib
	monodis $(OUTPUT_LIB)