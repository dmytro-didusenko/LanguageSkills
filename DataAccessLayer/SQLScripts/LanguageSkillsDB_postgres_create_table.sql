CREATE TABLE "Categories" (
	"Id" serial NOT NULL,
	"CategoryName" TEXT NOT NULL,
	"CategoryImagePath" TEXT NOT NULL,
	CONSTRAINT "Categories_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "SubCategories" (
	"Id" serial NOT NULL,
	"SubCategoryName" TEXT NOT NULL,
	"SubCategoryImagePath" TEXT NOT NULL,
	"CategoryId" integer NOT NULL,
	CONSTRAINT "SubCategories_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Words" (
	"Id" serial NOT NULL,
	"WordName" TEXT NOT NULL,
	"WordImagePath" TEXT NOT NULL,
	"SubCategoryId" integer NOT NULL,
	CONSTRAINT "Words_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Tests" (
	"Id" serial NOT NULL,
	"TestName" TEXT NOT NULL,
	CONSTRAINT "Tests_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "Languages" (
	"Id" serial NOT NULL,
	"ShortName" TEXT NOT NULL,
	"FullName" TEXT NOT NULL,
	CONSTRAINT "Languages_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "CategoryTranslations" (
	"Id" serial NOT NULL,
	"CategoryTranslationName" TEXT NOT NULL,
	"CategotyId" integer NOT NULL,
	"LanguageId" integer NOT NULL,
	CONSTRAINT "CategoryTranslations_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "SubCategoryTranslations" (
	"Id" serial NOT NULL,
	"SubCategoryTranslationName" TEXT NOT NULL,
	"SubCategoryId" integer NOT NULL,
	"LanguageId" integer NOT NULL,
	CONSTRAINT "SubCategoryTranslations_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "WordTranslations" (
	"Id" serial NOT NULL,
	"WordTranslationName" TEXT NOT NULL,
	"PronunciationPath" TEXT,
	"WordId" integer NOT NULL,
	"LanguageId" integer NOT NULL,
	CONSTRAINT "WordTranslations_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "TestTranslations" (
	"Id" serial NOT NULL,
	"TestTranslationName" TEXT NOT NULL,
	"TestId" integer NOT NULL,
	"LanguageId" integer NOT NULL,
	CONSTRAINT "TestTranslations_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);



CREATE TABLE "LanguageTranslations" (
	"Id" serial NOT NULL,
	"LanguageTranslationName" TEXT NOT NULL,
	"LanguageWordId" integer NOT NULL,
	"LanguageId" integer NOT NULL,
	CONSTRAINT "LanguageTranslations_pk" PRIMARY KEY ("Id")
) WITH (
  OIDS=FALSE
);




ALTER TABLE "SubCategories" ADD CONSTRAINT "SubCategories_fk0" FOREIGN KEY ("CategoryId") REFERENCES "Categories"("Id");

ALTER TABLE "Words" ADD CONSTRAINT "Words_fk0" FOREIGN KEY ("SubCategoryId") REFERENCES "SubCategories"("Id");



ALTER TABLE "CategoryTranslations" ADD CONSTRAINT "CategoryTranslations_fk0" FOREIGN KEY ("CategotyId") REFERENCES "Categories"("Id");
ALTER TABLE "CategoryTranslations" ADD CONSTRAINT "CategoryTranslations_fk1" FOREIGN KEY ("LanguageId") REFERENCES "Languages"("Id");

ALTER TABLE "SubCategoryTranslations" ADD CONSTRAINT "SubCategoryTranslations_fk0" FOREIGN KEY ("SubCategoryId") REFERENCES "SubCategories"("Id");
ALTER TABLE "SubCategoryTranslations" ADD CONSTRAINT "SubCategoryTranslations_fk1" FOREIGN KEY ("LanguageId") REFERENCES "Languages"("Id");

ALTER TABLE "WordTranslations" ADD CONSTRAINT "WordTranslations_fk0" FOREIGN KEY ("WordId") REFERENCES "Words"("Id");
ALTER TABLE "WordTranslations" ADD CONSTRAINT "WordTranslations_fk1" FOREIGN KEY ("LanguageId") REFERENCES "Languages"("Id");

ALTER TABLE "TestTranslations" ADD CONSTRAINT "TestTranslations_fk0" FOREIGN KEY ("TestId") REFERENCES "Tests"("Id");
ALTER TABLE "TestTranslations" ADD CONSTRAINT "TestTranslations_fk1" FOREIGN KEY ("LanguageId") REFERENCES "Languages"("Id");

ALTER TABLE "LanguageTranslations" ADD CONSTRAINT "LanguageTranslations_fk0" FOREIGN KEY ("LanguageWordId") REFERENCES "Languages"("Id");
ALTER TABLE "LanguageTranslations" ADD CONSTRAINT "LanguageTranslations_fk1" FOREIGN KEY ("LanguageId") REFERENCES "Languages"("Id");
